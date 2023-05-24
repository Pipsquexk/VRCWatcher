using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace VRCWatcher
{
    public class Logger
    {
        private static readonly object locker = new();
        private static Logger instance = null;

        public static Logger Instance
        {
            get
            {
                lock (locker)
                {
                    instance ??= new();
                    return instance;
                }
            }
        }

        public string currentLogFile = "NULL";

        private Logger() => Init();

        private void Init()
        {
            try
            {
                Console.WriteLine("Initializing Logger...");

                if (!Directory.Exists(FileManager.LogsDir)) Directory.CreateDirectory(FileManager.LogsDir);

                if (currentLogFile == "NULL")
                {
                    currentLogFile = $"{FileManager.LogsDir}\\{DateTime.Now.ToString("MM-dd-yyyy HH-mm-ss")} VRCWatcher.log";
                }

                Log("Initialized Logger!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Log(string txt)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");


            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(time);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");

            Console.WriteLine(txt);

            string fileLog = $"[{time}] {txt}";
            LogToFile(fileLog);
        }

        public void Log(string txt, ConsoleColor clr)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");


            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(time);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");

            Console.ForegroundColor = clr;
            Console.WriteLine(txt);
            Console.ForegroundColor = ConsoleColor.White;

            string fileLog = $"[{time}] {txt}\n";
            LogToFile(fileLog);
        }

        private void LogToFile(string txt) => File.AppendAllText(currentLogFile, txt);
    }
}
