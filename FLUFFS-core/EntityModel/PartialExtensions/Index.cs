using Hasher;
using Pri.LongPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EntityModel
{
    partial class Index
    {
        public bool IsRunning { get; set; }
        public int RunningFileCount { get; private set; } = 0;

        //Used only for binding in grids.
        public bool IsSelected { get; set; }

        private const string INVALID_ROOT_MESSAGE =
            "The specified root directory could not be found.";

        public void BuildIndexAsync(string root, string alias)
        {
            IsRunning = true;

            Thread thread = new Thread(() =>
            {
                if (Directory.Exists(root) == false)
                {
                    throw new System.IO.DirectoryNotFoundException(INVALID_ROOT_MESSAGE);
                }

                DirectoryInfo directory = new DirectoryInfo(root);
                TrackedFolder folder = new TrackedFolder()
                {
                    FullPath = directory.FullName,
                    Name = directory.Name
                };

                using (DbModelContainer db = new DbModelContainer())
                {
                    db.Indices.Add(new Index()
                    {
                        Alias = alias,
                        Root = folder
                    });
                    db.SaveChanges();
                }


                DigDirectory(directory, folder.Id);
                IsRunning = false;
            });

            thread.Start();
        }

        //TODO: put something in here about checking to cancel,
        //including adding an attribute to the entity model for
        //sys admin to mark.

        private void DigDirectory(DirectoryInfo directory, int parentID)
        {
            IEnumerable<FileInfo> files;
            IEnumerable<DirectoryInfo> subFolders;


            List<TrackedFile> trackedFiles = new List<TrackedFile>();
            try
            {
                files = directory.EnumerateFiles();
                foreach (FileInfo file in files)
                {
                    trackedFiles.Add(new TrackedFile()
                    {
                        Name = file.Name,
                        FullPath = file.FullName,
                        Created = file.CreationTime,
                        LastSeen = file.LastWriteTime,
                        Extension = file.Extension,
                        TrackedFolderId = parentID,
                        Length = file.Length,
                        TrackForUpdates = false
                    });
                    
                    
                    RunningFileCount++;
                }
            }
            catch (Exception)
            {
                //some exceptions for protected files etc.
                //nothing to do with it really.
            }

            List<TrackedFolder> trackedFolders = new List<TrackedFolder>();

            try
            {
                subFolders = directory.EnumerateDirectories();
                foreach (DirectoryInfo folder in subFolders)
                {
                    trackedFolders.Add(new TrackedFolder()
                    {
                        Name = folder.Name,
                        FullPath = folder.FullName,
                        TrackedFolderId = parentID,
                    });
                }
            }
            catch (Exception)
            {
                //as per for file.
            }

            using (DbModelContainer db = new DbModelContainer())
            {
                db.TrackedFolders.AddRange(trackedFolders);
                db.TrackedFiles.AddRange(trackedFiles);
                db.SaveChanges();
            }

            foreach (TrackedFolder trackedFolder in trackedFolders)
            {
                DirectoryInfo subFolder = new DirectoryInfo(trackedFolder.FullPath);
                DigDirectory(subFolder, trackedFolder.Id);
            }            

        }
    }
}
