using System;
using System.IO;

namespace OwnLog
{
  public class Logger
  {
    private readonly string _directorypath;
    private readonly string _filepath;

    /// <summary>
    /// Creates OwnLog.Logger at current location with the default name Log-yyyy-MM-dd-HH-mm.txt.
    /// </summary>
    public Logger() : this("Log")
    {
    }

    /// <summary>
    /// Creates OwnLog.Logger at current location.
    /// </summary>
    /// <param name="name">Specifies the name before the date used in the filename.</param>
    public Logger(string name) : this(name, "")
    {
    }

    /// <summary>
    /// Creates OwnLog.Logger.
    /// </summary>
    /// <param name="name">Specifies the name before the date used in the filename.</param>
    /// <param name="folder">Specifies the folder. Can be relative or absolute.</param>
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

    /// <summary>
    /// Write a warning to your log.
    /// </summary>
    /// <param name="message">The message to write.</param>
    /// <param name="args">Params like in string.Format().</param>
    public void Warn(string message, params object[] args)
    {
      message = string.Format(message, args);
      Write($"{DateTime.Now} - WARNING: {message}");
    }

    /// <summary>
    /// Write a info to your log.
    /// </summary>
    /// <param name="message">The message to write.</param>
    /// <param name="args">Params like in string.Format().</param>
    public void Info(string message, params object[] args)
    {
      message = string.Format(message, args);
      Write($"{DateTime.Now} - INFO:    {message}");
    }

    /// <summary>
    /// Write an error to your log.
    /// </summary>
    /// <param name="message">The message to write.</param>
    /// <param name="args">Params like in string.Format().</param>
    public void Error(string message, params object[] args)
    {
      message = string.Format(message, args);
      Write($"{DateTime.Now} - ERROR:   {message}");
    }

    /// <summary>
    /// Write to file.
    /// </summary>
    /// <param name="message"></param>
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
