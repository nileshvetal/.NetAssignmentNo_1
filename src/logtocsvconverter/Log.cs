using System;
using System.Collections.Generic;
using System.IO;

namespace logtocsvconverter
{
    class Log
    {
        public Log()
        {
            Lines = new List<string>();
        }

        public List<string> Lines { get; set; }


        public void ReadLines(string path, List<string> levels)
        {
            if (!Directory.Exists(path))
                Console.WriteLine("Please provide correct direct path");

            string[] files;
            try
            {
                files = Directory.GetFiles(path, "*.log");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            var line = "";
            foreach (var file in files)
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (levels.Count == 0 && line.Length > 20)
                        {
                            Lines.Add(line);
                        }
                        else
                        {
                            foreach (var level in levels)
                            {
                                if (line.ToLower().Contains(level.ToLower()))
                                {
                                    Lines.Add(line);
                                }
                            }
                        }

                    }
                }
            }
        }

    }
}