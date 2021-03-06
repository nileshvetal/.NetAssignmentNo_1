using System.Collections.Generic;
using System;
using System.IO;

namespace logtocsvconverter
{
    public class InputArguments
    {
        public InputArguments()
        {
            this.LogLevels = new List<string>();
            this.LogDir = "";
            this.OutPath = "";
        }

        public string LogDir { get; set; }

        public List<string> LogLevels { get; set; }

        public string OutPath { get; set; }

        public void Get(string[] commands)
        {
            for (var index = 0; index < commands.Length - 1; index += 2)
            {

                switch (commands[index])
                {
                    case "--log-dir":
                        if (!commands[index + 1].Contains("\\"))
                            Console.WriteLine("Please provide correct path");

                        LogDir = commands[index + 1];
                        break;

                    case "--log-level":
                        if (!(commands[index + 1].ToLower().Equals("trace") || commands[index + 1].ToLower().Equals("info")
                                || commands[index + 1].ToLower().Equals("warn") || commands[index + 1].ToLower().Equals("debug")))
                            Console.WriteLine("Please provode correct log level");
                        LogLevels.Add(commands[index + 1]);
                        break;

                    case "--csv":
                        if (!commands[index + 1].Contains("\\"))
                            Console.WriteLine("Please provide correct path formate");

                        if (!Directory.Exists(commands[index + 1]))
                        {
                            Console.WriteLine("Please provide correct output path");
                        }

                        OutPath = commands[index + 1];
                        break;

                    default:
                        Console.WriteLine("Please enter valid command");
                        return;

                }
            }
        }

    }
}