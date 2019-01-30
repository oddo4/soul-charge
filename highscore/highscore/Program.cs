using Library;
using System;

namespace highscore
{
    class Program
    {
        static void Main(string[] args)
        {
            bool loop = true;
            FileHelper fileHelper = new FileHelper();
            fileHelper.FilePath = "D:/source/repos/bounlfi15/soul-charge/highscore/highscoreWPF/bin/Debug/HighScoreData.csv";

            //Console.WriteLine("Hello World!");

            while (loop)
            {
                Console.Write("Enter path to CSV: ");
                var inputPath = Console.ReadLine();

                Console.WriteLine("Checking...");
                var list = fileHelper.ReadCSVFile(inputPath);

                if (list != null)
                {
                    Console.WriteLine("Creating new data...");
                    fileHelper.WriteCSVFile(list);
                    loop = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Could not load data. File corrupted or not compatible.");
                }
            }

            Console.WriteLine("Complete! Press any key to exit...");
            Console.ReadKey();
            Console.WriteLine("Exiting...");
        }
    }
}
