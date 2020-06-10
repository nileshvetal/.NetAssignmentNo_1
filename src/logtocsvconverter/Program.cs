using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace logtocsvconverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter command");
            var inputCommand = Console.ReadLine();
            var commands = inputCommand.Split(" ");

            if (!ValidateCommand(string.Join(" ", commands)))
            {
                Console.WriteLine("Invalide command formate");
                return;
            }

            var inputArguments = new InputArguments();
            inputArguments.Get(commands);

            var log = new Log();
            log.ReadLines(inputArguments.LogDir, inputArguments.LogLevels);

            var csvFormats = LogToCSV(log);

            WriteToCSVFile(csvFormats, inputArguments.OutPath);

            Console.WriteLine("Successfully converted log file to csv file");

        }

        private static bool ValidateCommand(string command)
        {
            var isValid = true;
            if (!command.Contains("--log-dir") || !command.Contains("--csv"))
                isValid = false;

            return isValid;
        }

        private static List<CSVFormat> LogToCSV(Log log)
        {
            var csvFormats = new List<CSVFormat>();
            int index = 0;

            foreach (var line in log.Lines)
            {
                var csvformate = new CSVFormat();
                var dateValue = new DateTime();

                var lineAfterSplit = line.Split(":.");

                var dateString = lineAfterSplit[0].Split(" ")[0] + "/2020 " + lineAfterSplit[0].Split(" ")[1];
                try
                {
                    dateValue = DateTime.Parse(dateString);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to convert {dateString} into date.");
                }

                csvformate.Date = string.Format("{0:dd MMM yyyy}", dateValue);
                csvformate.Time = dateValue.ToShortTimeString();
                csvformate.Level = lineAfterSplit[0].Split(" ")[2];
                csvformate.Text = lineAfterSplit[1].Replace("..", "").Replace(", ", ",");
                csvformate.Number = ++index;
                csvFormats.Add(csvformate);
            }

            return csvFormats;
        }

        public static void WriteToCSVFile(List<CSVFormat> csvFormats, string outPath)
        {
            var csvFile = outPath + "\\sortedLog.csv";
            csvFormats = csvFormats.OrderBy(csv => csv.Level).ToList();
            foreach (var csvFormat in csvFormats)
            {
                using (var writer = File.AppendText(csvFile))
                {
                    writer.WriteLine(csvFormat.Number + "," + csvFormat.Level
                                    + "," + csvFormat.Date + "," + csvFormat.Time + "," + csvFormat.Text);
                }
            }
        }

    }
}
