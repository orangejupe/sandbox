using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Regex logEntry = new Regex(@"(?<Date>\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2},\d{3})");
            string regex =
                @"(?<Date>\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2},\d{3})\s(?<Thread>\[@THREAD_NAME\])\s(?<Level>INFO|WARN|ERROR)\s(?<Rest>.*)";
            Regex parse = new Regex(regex.Replace("@THREAD_NAME", threadName), RegexOptions.Compiled);
            FilesEnumerator files = new FilesEnumerator(name);
            using (StreamWriter writer = File.CreateText(string.Format("{0}.log", threadName)))
            {
                foreach (FileInfo info in files)
                {
                    Console.WriteLine("Tool started:\n\tFile: {0}\n\tThread: {1}", info.FullName, threadName);
                    using (StreamReader reader = info.OpenText())
                    {
                        bool write = false;
                        string line = string.Empty;
                        string output = string.Empty;

                        while (reader.Peek() != -1)
                        {
                            line = reader.ReadLine();
                            if (logEntry.Match(line).Success)
                            {
                                Match match = parse.Match(line);
                                if (match.Success)
                                {
                                    write = true;
                                    output = String.Format(
                                        "{0}\t{1}\t{2}\t{3}",
                                        match.Groups["Date"],
                                        match.Groups["Thread"],
                                        match.Groups["Level"],
                                        match.Groups["Rest"].ToString().Trim());
                                }
                                else
                                {
                                    write = false;
                                }
                            }
                            else
                            {
                                output = String.Format("\t\t\t{0}", line.Trim('\t', ' '));
                            }
                            if (write)
                            {
                                writer.WriteLine(output);
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Tool finished.");
            Console.ReadKey();
        }
    }
}