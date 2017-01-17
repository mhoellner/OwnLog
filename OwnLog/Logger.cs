using System;
using System.IO;

namespace OwnLog
{
    public class Logger
    {
      private string _directorypath;
      private string _filepath;

      public Logger() : this("Log")
      {
      }

      public Logger(string name) : this(name, "")
      {
      }

      public Logger(string name, string folder)
      {
        name = name + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".txt";
        if (string.IsNullOrEmpty(folder))
        {
          _directorypath = "";
          _filepath = name;
        }
        else
        {
          _directorypath = folder;
          _filepath = folder + "\\" + name;
        }
    }

      public void Warn(string message, params object[] args)
      {
        message = string.Format(message, args);
        Write($"{DateTime.Now} - WARNING: {message}");
      }

      public void Info(string message, params object[] args)
      {
        message = string.Format(message, args);
        Write($"{DateTime.Now} - INFO:    {message}");
      }

      public void Error(string message, params object[] args)
      {
        message = string.Format(message, args);
        Write($"{DateTime.Now} - ERROR:   {message}");
      }

      private void Write(string message)
      {
        if (!File.Exists(_filepath))
        {
          Directory.CreateDirectory(_directorypath);
          using (StreamWriter sw = File.CreateText(_filepath))
          {
            sw.WriteLine(message);
          }
        }
        else
        {
          using (StreamWriter sw = File.AppendText(_filepath))
          {
            sw.WriteLine(message);
          }
        }
      }
    }
}
