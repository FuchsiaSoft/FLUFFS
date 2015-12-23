using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EntityModel;

namespace Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread searchThread = new Thread(() =>
            {
                SearchJobLoop();
            });

            searchThread.Start();
        }

        private static void SearchJobLoop()
        {
            IEnumerable<SearchJob> searchJobs;

            using (DbModelContainer db = new DbModelContainer())
            {
                searchJobs = db.SearchJobs
                    .Where(s => s.Status == SearchStatus.Paused ||
                                s.Status == SearchStatus.Pending)
                    .ToList();

            }

            foreach (SearchJob job in searchJobs)
            {
                int threadCount = Properties.Settings.Default.SearchThreadCount;

                job.Start(threadCount);

                do
                {
                    Thread.Sleep(1000);
                } while (job.Status == SearchStatus.Running);
            }
        }
    }
}
