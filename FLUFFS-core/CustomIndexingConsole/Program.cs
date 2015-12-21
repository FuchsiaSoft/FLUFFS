using EntityModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomIndexingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> roots = new List<string>()
            {
                "\\\\netapp04-cifs\\LCC001",
                "\\\\netapp04-cifs\\LCC002",
                "\\\\netapp04-cifs\\LCC003",
                "\\\\netapp04-cifs\\LCC004",
                "\\\\netapp04-cifs\\LCC005",
                "\\\\netapp04-cifs\\LCC006",
                "\\\\netapp04-cifs\\LCC007",
                "\\\\netapp04-cifs\\LCC008",
                "\\\\netapp04-cifs\\LCC009",
                "\\\\netapp04-cifs\\LCC010",
                "\\\\netapp04-cifs\\LCC011",
                "\\\\netapp04-cifs\\LCC012",
                "\\\\netapp04-cifs\\LCC013",
                "\\\\netapp04-cifs\\LCC014",
                "\\\\netapp04-cifs\\LCC015",
                "\\\\netapp04-cifs\\LCC016",
                "\\\\netapp04-cifs\\LCC017",
                "\\\\netapp04-cifs\\LCC018",
                "\\\\netapp04-cifs\\LCC019",
                "\\\\netapp04-cifs\\LCC020",
                "\\\\netapp04-cifs\\homedata"
            };

            Stopwatch watch = Stopwatch.StartNew();

            List<Index> indices = new List<Index>();

            foreach (string root in roots)
            {
                Index index = new Index();
                index.IsRunning = true;
                index.Alias = root;
                index.BuildIndexAsync(root, root);
                indices.Add(index);
            }

            do
            {
                Console.Clear();
                Console.WriteLine("##########################");
                Console.WriteLine();

                foreach (Index index in indices)
                {
                    Console.WriteLine(index.Alias + "  -  " + index.RunningFileCount.ToString("#,###") + " files " +
                                        (index.IsRunning == true ? "  -  Running" : "  -  Finished"));
                }

                Console.WriteLine();
                Console.WriteLine("Total Files:  " + indices.Sum(s => s.RunningFileCount).ToString("#,###"));
                Console.WriteLine("Total Time:   " + watch.Elapsed.TotalHours.ToString("#,##0.00") + " hours");
                Console.WriteLine("Total FPS:    " + ((double)(indices.Sum(s => s.RunningFileCount)) / (double)(watch.Elapsed.TotalSeconds)).ToString("#,##0.#"));
                Console.WriteLine();

                Console.WriteLine("##########################");

                Thread.Sleep(1000);

            } while (SomeAreRunning(indices));

            Console.WriteLine("All finished");
            Console.ReadLine();
        }

        private static bool SomeAreRunning(List<Index> indices)
        {
            foreach (Index index in indices)
            {
                if (index.IsRunning)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
