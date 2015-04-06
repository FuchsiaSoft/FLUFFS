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
        private const string INVALID_ROOT_MESSAGE =
            "The specified root directory could not be found.";

        public void BuildIndexAsync(string root, string alias)
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

            Thread thread = new Thread(() =>
            {
                DigDirectory(directory, folder.Id);
            });
            thread.Start();
        }

        //TODO: put something in here about checking to cancel,
        //including adding an attribute to the entity model for
        //sys admin to mark.

        private void DigDirectory(DirectoryInfo directory, int parentID)
        {
            //might not be able to enumerate files for like
            //a million reasons, as per design docs it
            //doesn't matter too much if not able to get
            //to some files, so do a quick TryCatch to see
            //if there are any issues
            FileInfo[] files;
            DirectoryInfo[] subFolders;

            try
            {
                files = directory.GetFiles();
                subFolders = directory.GetDirectories();
            }
            catch (Exception)
            {
                return;
            }

            List<TrackedFile> trackedFiles = new List<TrackedFile>();
            for (int i = 0; i < files.Count()-1; i++)
            {
                try
                {
                    FileInfo file = files[i];
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
                }
                catch (Exception)
                {
                    //some exceptions for protected files etc.
                    //nothing to do with it really.
                }
            }

            List<TrackedFolder> trackedFolders = new List<TrackedFolder>();
            for (int i = 0; i < subFolders.Count() - 1; i++)
            {
                try
                {
                    DirectoryInfo folder = subFolders[i];
                    trackedFolders.Add(new TrackedFolder()
                    {
                        Name = folder.Name,
                        FullPath = folder.FullName,
                        TrackedFolderId = parentID,
                    });
                }
                catch (Exception)
                {
                    //as per for file.
                }
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
