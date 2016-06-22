namespace App
{
    using System;
    using System.IO;

    public static class ArgumentParser
    {
        public static Settings Parse(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ApplicationException("Invalid param count: app.exe path thread_name");
            }
            Settings settings = new Settings();
            settings.DirectoryName = args[0];
            if (!Directory.Exists())
            {
                Console.WriteLine();
                throw new ApplicationException($"Directory {}");
            }
            string threadName = args[1];
        }
    }
}