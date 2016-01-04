using CollectionSplitter;
using FileDigger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EntityModel
{
    partial class SearchJob
    {
        /// <summary>
        /// Internal flag for whether or not the search loops
        /// should continue
        /// </summary>
        private bool _KeepRunning = true;

        /// <summary>
        /// A private pool of worker threads used to allocate
        /// files for different threads.  Held at object scope
        /// to allow all necessary methods to interact with it.
        /// </summary>
        private IList<Thread> _WorkerThreads = new List<Thread>();

        /// <summary>
        /// Changes the status of the search job, and updates
        /// the DB to match.
        /// </summary>
        /// <param name="newStatus">The new status</param>
        private void ChangeStatus(SearchStatus newStatus)
        {
            using (DbModelContainer db = new DbModelContainer())
            {
                db.SearchJobs.Attach(this);

                this.Status = newStatus;

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Starts the search single threaded.
        /// </summary>
        public void Start()
        {
            Start(1);
        }

        /// <summary>
        /// Starts the search with the specified threadcount
        /// </summary>
        /// <param name="threadCount">The number of threads
        /// to split the work across</param>
        public void Start(int threadCount)
        {
            
            if (this.Status == SearchStatus.Running)
            {
                throw new InvalidOperationException("The search job is already running");
            }            

            if (this.Status == SearchStatus.Cancelled)
            {
                throw new InvalidOperationException("Cannot start a cancelled search job");
            }

            if (this.Status == SearchStatus.Completed)
            {
                throw new InvalidOperationException("The search job is already completed");
            }

            if (this.Status == SearchStatus.Rejected)
            {
                throw new InvalidOperationException("Cannot start a rejected search job");
            }

            _KeepRunning = true;

            ChangeStatus(SearchStatus.Running);

            MainCycle(threadCount);
        }

        /// <summary>
        /// Stops the search job, but does not cancel it.
        /// If this is called the current instance of the search
        /// will terminate but it will resume next time the
        /// crawler looks for outstanding searches to be run
        /// </summary>
        public void Pause()
        {
            _KeepRunning = false;

            WaitForThreadsToFinish();

            ChangeStatus(SearchStatus.Paused);
        }

        /// <summary>
        /// Cancels the search job, it will never be picked up
        /// again by the crawler if this method is called until
        /// the search job has its status changed
        /// </summary>
        public void Cancel()
        {
            _KeepRunning = false;

            WaitForThreadsToFinish();

            ChangeStatus(SearchStatus.Cancelled);
        }

        /// <summary>
        /// Method is just a blocker until all worker threads
        /// have finished their current files, here to prevent
        /// the Status property being set before the files have actually
        /// finished processing.
        /// </summary>
        private void WaitForThreadsToFinish()
        {
            do
            {
                Thread.Sleep(5000);

            } while (_WorkerThreads.Count(t => t.IsAlive) > 0);
        }

        /// <summary>
        /// The main cycle/loop that handles the processing of files
        /// </summary>
        /// <param name="threadCount">The number of threads to split
        /// the work across</param>
        private void MainCycle(int threadCount)
        {
            do
            {
                IEnumerable<TrackedFile> outstandingFiles = GetOutstandingFiles();

                if (outstandingFiles.Count() == 0)
                {
                    ChangeStatus(SearchStatus.Completed);
                    return;
                }

                IEnumerable<IEnumerable<TrackedFile>> masterList = 
                    SplitFiles(outstandingFiles, threadCount);

                _WorkerThreads.Clear();

                foreach (IEnumerable<TrackedFile> subList in masterList)
                {
                    Thread thread = new Thread(() =>
                    {
                        ProcessSubList(subList);
                    });

                    thread.Start();

                    _WorkerThreads.Add(thread);
                }

                WaitForThreadsToFinish();

            } while (_KeepRunning);


        }

        /// <summary>
        /// Dies the actual work of processing the files for the search
        /// </summary>
        /// <param name="subList"></param>
        private void ProcessSubList(IEnumerable<TrackedFile> subList)
        {
            IEnumerable<string> regExToCheck = GetRegExToCheck();

            IEnumerable<string> stringsToCheck = GetStringsToCheck();

            foreach (TrackedFile file in subList)
            {
                try
                {
                    IFileReader reader = new FileReader(file.FullPath);
                    
                    if (_KeepRunning == false) break;

                    if (reader.CheckRegEx(regExToCheck))
                    {
                        CategoriseFile(file);
                    }
                    else
                    {
                        if (reader.CheckString(stringsToCheck))
                        {
                            CategoriseFile(file);
                        }
                    }
                }
                catch (Exception e)
                {
                    //do nothing at the minute
                    //
                    //TODO: implement something for error logging
                    try
                    {
                        System.IO.File.AppendAllText("C:\\Temp\\Crawler\\Logs\\BadLog.txt", e.Message + Environment.NewLine +
                                                Environment.NewLine + e.StackTrace + Environment.NewLine +
                                                "############################################" + Environment.NewLine);
                    }
                    catch (Exception)
                    {
                        //can't always get to the file with multi threading, can't be bothered
                        //to deal with it properly right now
                    }
                    
                }
                finally
                {
                    MarkFileDone(file);
                }
            }
            
        }

        /// <summary>
        /// Marks the file as completed so that it doesn't get interrogated repeatedly
        /// </summary>
        /// <param name="file"></param>
        private void MarkFileDone(TrackedFile file)
        {
            using (DbModelContainer db = new DbModelContainer())
            {
                //Remove was giving poor performance for large result
                //sets, presumably because of how EF handles junction
                //tables, so a SPROC does it.

                db.MarkFileDone(file.Id, this.Id);
            }
        }

        /// <summary>
        /// Categorises the file as having matches at least
        /// one of the supplied regexes or strings, using
        /// the category assigned to this search job
        /// </summary>
        /// <param name="file">the file to categorise</param>
        private void CategoriseFile(TrackedFile file)
        {
            using (DbModelContainer db = new DbModelContainer())
            {
                //Can't use attach here as was getting exceptions to do
                //with EF graph tracking.  I think the way that the 
                //converter works between the SPROC for getting due files
                //and tracked files causes the issue, not a major issue
                //to do it this way though, has no other bearing on the
                //rest of the code

                TrackedFile fileToMark = db.TrackedFiles.Find(file.Id);
                SearchJob job = db.SearchJobs.Find(this.Id);
                Category category = db.Categories.Find(job.Category.Id);

                fileToMark.Categories.Add(category);

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the list of simple strings to check against
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetRegExToCheck()
        {
            using (DbModelContainer db = new DbModelContainer())
            {
                //See comments in GetStringsToCheck

                return db.SearchJobs.Find(Id).Regexes
                            .Select(s => s.Content).ToList();
            }
        }

        /// <summary>
        /// Gets the list of regexes to check against
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetStringsToCheck()
        {
            using (DbModelContainer db = new DbModelContainer())
            {
                //can't use attach here but need to search manually because
                //multiple threads will be working from the same object, and
                //we can't have multiple db contexts attaching the same
                //entity, this is not the same issue as for MarkFileDone

                return db.SearchJobs.Find(Id).SearchStrings
                            .Select(s => s.Content).ToList();
            }
        }

        /// <summary>
        /// Splits the list of oustanding files to a number of sublists to be worked
        /// through by worker threads.
        /// </summary>
        /// <param name="outstandingFiles">The original list of files</param>
        /// <param name="threadCount">The number of sublists to create</param>
        /// <returns></returns>
        private IEnumerable<IEnumerable<TrackedFile>> 
            SplitFiles(IEnumerable<TrackedFile> outstandingFiles, int subListCount)
        {
            CollectionSplitter<TrackedFile> splitter = new CollectionSplitter<TrackedFile>();
            return splitter.SplitCollection(outstandingFiles, subListCount);
        }

        /// <summary>
        /// Checks for a list of outstanding files and returns them
        /// if found.  This method only returns the top 1000 of pending files
        /// in order to deal with performance issues with EF
        /// </summary>
        /// <returns>An IEnumerable of outstanding files to process</returns>
        private IEnumerable<TrackedFile> GetOutstandingFiles()
        {
            using (DbModelContainer db = new DbModelContainer())
            {
                db.Database.CommandTimeout = 120;
                
                IEnumerable<TrackedFilesDue_Result> procedureResults 
                    = db.TrackedFilesDue(1000, this.Id);

                List<TrackedFile> fileResults = new List<TrackedFile>();

                foreach (TrackedFilesDue_Result result in procedureResults)
                {
                    fileResults.Add((TrackedFile)result);
                }

                return fileResults;

                //SearchJob job = db.SearchJobs.Find(this.Id);

                //List<TrackedFile> workingFiles = new List<TrackedFile>();

                //IEnumerable<TrackedFile> pendingFiles = job.PendingFiles;

                //foreach (TrackedFile file in job.PendingFiles)
                //{
                //    workingFiles.Add(file);
                //    if (workingFiles.Count == 1000) break;
                //}

                //return workingFiles;
                
            }
        }
    }
}
