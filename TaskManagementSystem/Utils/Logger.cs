using System;
using System.IO;

namespace TaskManagementSystem.Utils
{
    public class Logger
    {
        public static void WriteException(string fullPath, Exception e)
        {
            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {
                sw.Write($"\nData: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\n");
                sw.Write($"Mensagem: {e.Message}\n");
                sw.Write($"StackTrace: {e.StackTrace}");
            }
        }
    }
}