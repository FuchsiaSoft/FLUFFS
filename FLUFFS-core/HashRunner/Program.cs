using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel;
using Hasher;
using File = Pri.LongPath.File;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace HashRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            int totalProcessed = 0;
            int totalHashed = 0;
            int totalFailed = 0;


            IEnumerable<FilesDueForHash_Result> filesToHash = GetNextFiles();

            do
            {
                Parallel.ForEach(filesToHash, fileToHash =>
                {
                    Console.WriteLine("Processing: " + fileToHash.FullPath);

                    try
                    {
                        if (File.Exists(fileToHash.FullPath) && 
                            fileToHash.Length > 0)
                        {
                            Stream fileStream = File.OpenRead(fileToHash.FullPath);

                            IHashMaker hasher = new HashMaker();

                            string preHash = hasher.GetPreHash(fileToHash.FullPath);
                            string md5 = hasher.GetMD5(fileStream);
                            string sha1 = hasher.GetSHA1(fileStream);

                            using (DbModelContainer db = new DbModelContainer())
                            {
                                db.Database.CommandTimeout = 60;

                                TrackedFile fileToUpdate = db.TrackedFiles.Find(fileToHash.Id);
                                fileToUpdate.PreHash = preHash;
                                fileToUpdate.MD5 = md5;
                                fileToUpdate.SHA1 = sha1;

                                db.SaveChanges();
                            }

                            Interlocked.Increment(ref totalHashed);
                        } 
                    }
                    catch (Exception)
                    {
                        //mark it as failed
                        Interlocked.Increment(ref totalFailed);
                    }
                    finally
                    {
                        Interlocked.Increment(ref totalProcessed);

                        using (DbModelContainer db = new DbModelContainer())
                        {
                            db.Database.CommandTimeout = 60;
                            TrackedFile file = db.TrackedFiles.Find(fileToHash.Id);
                            file.HashAttempted = DateTime.Now;
                            db.SaveChanges();
                        }
                    }
                });

                filesToHash = GetNextFiles();

            } while (filesToHash.Count() > 0);

        }

        private static IEnumerable<FilesDueForHash_Result> GetNextFiles()
        {
            IEnumerable<FilesDueForHash_Result> filesToHash;
            using (DbModelContainer db = new DbModelContainer())
            {
                db.Database.CommandTimeout = 600;
                filesToHash = db.FilesDueForHash().ToList();
            }

            return filesToHash;
        }
    }
}
