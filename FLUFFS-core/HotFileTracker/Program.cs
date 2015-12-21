using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel;
using CollectionSplitter;
using System.Threading;
using FileInfo = Pri.LongPath.FileInfo;

namespace HotFileTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<TrackedFile> masterList = GetFilesToTrack();

            int threadCount = Properties.Settings.Default.ThreadCount;

            IEnumerable<IEnumerable<TrackedFile>> listOfLists =
                SplitMasterList(masterList, threadCount);

            DoInfiniteLoop(listOfLists);
        }

        private static void DoInfiniteLoop(IEnumerable<IEnumerable<TrackedFile>> listOfLists)
        {
            do
            {
                List<Thread> workerThreads = new List<Thread>();

                foreach (IEnumerable<TrackedFile> subList in listOfLists)
                {
                    Thread thread = new Thread(() =>
                    {
                        CheckFilesForUpdates(subList);
                    });

                    thread.Start();

                    workerThreads.Add(thread);
                }

                do
                {
                    //just wait a bit and check again, do nothing
                    //until all of the threads have finished
                    Thread.Sleep(1500);

                } while (workerThreads.Count(t => t.IsAlive) > 0); 

            } while (true); //infinite loop for tracking files
        }

        private static void CheckFilesForUpdates(IEnumerable<TrackedFile> subList)
        {
            foreach (TrackedFile file in subList)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(file.FullPath);

                    if (fileInfo.Exists)
                    {
                        if (fileInfo.LastWriteTime > file.LastSeen ||
                            fileInfo.Length != file.Length)
                        {
                            //a change seen in modified date or file length
                            //are taken as a change to the tracked file, so
                            //log an update which will also update those values
                            LogFileChange(file, fileInfo.Length, fileInfo.LastWriteTime);
                        }
                    }

                }
                catch (Exception) { /**do nothing**/ }
            }
        }

        private static void LogFileChange(TrackedFile file, long newLength, DateTime newModifiedDate)
        {
            using (DbModelContainer db = new DbModelContainer())
            {
                db.Database.CommandTimeout = 1800;

                db.TrackedFiles.Attach(file);

                file.Length = newLength;
                file.LastSeen = newModifiedDate;

                if (file.UpdatesSeen == null || file.UpdatesSeen == 0)
                {
                    file.UpdatesSeen = 1;
                }
                else
                {
                    file.UpdatesSeen = file.UpdatesSeen + 1;
                }

                db.SaveChanges();
            }
        }

        private static IEnumerable<IEnumerable<TrackedFile>> 
            SplitMasterList(IEnumerable<TrackedFile> masterList,
                            int threadCount)
        {
            CollectionSplitter<TrackedFile> splitter = new CollectionSplitter<TrackedFile>();

            return splitter.SplitCollection(masterList, threadCount);
        }

        private static IEnumerable<TrackedFile> GetFilesToTrack()
        {
            using (DbModelContainer db = new DbModelContainer())
            {
                db.Database.CommandTimeout = 1800;

                IEnumerable<TrackedFile> filesToTrack =
                    db.TrackedFiles.Where(f => f.TrackForUpdates == true)
                        .ToList();

                return filesToTrack;
            }
        }
    }
}
