
//
// Copyright 2017 Paul Perrone.  All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IDA.Logging
{
    public class ApplicationLogging
    {
        /// <summary>
        /// Initialize log file directory, name, and full path according to input
        /// parameter.
        /// </summary>
        /// <param name="logDir">Directory containing the log file which is created
        /// if it does not exist.</param>
        /// <param name="logFile">Name of log file which is created if it does not exist
        /// when the first line is appended to the log.</param>
        public ApplicationLogging(string logDir, string logFile)
        {
            if (logDir.EndsWith("\\")) 
                LogDirectory = logDir; 
            else 
                LogDirectory = logDir + "\\";
            LogFileName = logFile;
            LogFilePath = LogDirectory + LogFileName;
            DoLogging = false;

            if (!Directory.Exists(LogDirectory)) Directory.CreateDirectory(LogDirectory);
        }
        /// <summary>
        /// Initialize log file directory, name, and full path to the default.  The directory is 
        /// created if it does not exist.  If the log file does not exist when the first line 
        /// is appended to the log then it is created.
        /// System.Environment.GetEnvironmentVariable("TEMP") + "\\IDA\\Logging\\", "Application.log"
        /// </summary>
        public ApplicationLogging() :
            this(LogDirectoryDefault, LogFileNameDefault)
        {}

        /// <summary>
        /// Default value used for LogDirectory if not specified as a constructor argument.
        /// </summary>
        public static string LogDirectoryDefault = System.Environment.GetEnvironmentVariable("TEMP") + "\\IDA\\Logging\\";
        /// <summary>
        /// Default value used for LogFileNmae if not specified as a constructor argument. 
        /// </summary>
        public static string LogFileNameDefault = "Application.log";

        /// <summary>
        /// Directory path containing log file.  The directory will be created if it 
        /// does not exist.
        /// </summary>
        public string LogDirectory { get; protected set; }
        /// <summary>
        /// Name of log file.  This file will be created if it does not exist when
        /// the first line is appended to the log.
        /// </summary>
        public string LogFileName { get; protected set; }
        /// <summary>
        /// Full path of the log file
        /// </summary>
        public string LogFilePath { get; protected set; }
        /// <summary>
        /// Controls if logging will be performed (true) or not (false).
        /// </summary>
        public bool DoLogging { get; set; }

        /// <summary>
        /// Logs an error to the log file then throws an exception.
        /// </summary>
        /// <param name="msg">Message to be appended to log.</param>
        /// <param name="sender">Object which sent ThrowError</param>
        public void ThrowError(string msg, object sender)
        {
            LogError(msg, sender);
            throw new System.Exception(msg);
        }
        /// <summary>
        /// Appends a message annotated as error to the log file.
        /// File and/or directory are created if necessary.
        /// </summary>
        /// <param name="msg">Message to be appended to log.</param>
        /// <param name="sender">Object which sent LogError.</param>
        public void LogError(string msg, object sender)
        {
            LogMessage("*** ERROR ***: " + msg, sender);
        }
        /// <summary>
        /// Appends a message annotated as warning to the log file.
        /// File and/or directory are created if necessary.
        /// </summary>
        /// <param name="msg">Message to be appended to log.</param>
        /// <param name="sender">Object which sent LogWarning.</param>
        public void LogWarning(string msg, object sender)
        {
            LogMessage("-- WARNING --: " + msg, sender);
        }
        /// <summary>
        /// Appends a message annotated as information to the log file.
        /// File and/or directory are created if necessary.
        /// </summary>
        /// <param name="msg">Message to be appended to log.</param>
        /// <param name="sender">Object which sent LogMessage.</param>
        public void LogInformation(string msg, object sender)
        {
            LogMessage("    INFO     : " + msg, sender);
        }
        /// <summary>
        /// Appends a message the log file with header line that includes date, time, 
        /// and object type.  Does not distinguish message as an error, warning, or info.
        /// File and/or directory are created if necessary.
        /// </summary>
        /// <param name="msg">Message to be appended to log.</param>
        /// <param name="sender">Object which sent LogMessage.</param>
        public void LogMessage(string msg, object sender)
        {
            WriteLineToLog(System.DateTime.Now.ToString() + " | " + sender.ToString() + System.Environment.NewLine +
                           msg + System.Environment.NewLine);
        }
        /// <summary>
        /// If logging is turned on via the property DoLogging, append additional 
        /// text to a file.  An additional NewLine is added to the end of the text provided.
        /// File and/or directory are created if necessary.
        /// </summary>
        /// <param name="line">Text to be appended to file.</param>
        public void WriteLineToLog(string line)
        {
            if (DoLogging) 
            {
                FileAppendLine(line, LogFilePath);
            }
        }
        /// <summary>
        /// Append additional text to a file. An additional NewLine is added 
        /// to the end of the text provided.
        /// </summary>
        /// <param name="line">Text to be appended to file.</param>
        /// <param name="filepath">Full path of file to be appended to.  
        /// File is created if necessary.  Directory must already exist</param>
        public static void FileAppendLine(string line, string filepath)
        {
            using (StreamWriter logWriter = new StreamWriter(filepath, true))
            {
                logWriter.WriteLine(line);
            }
        }
    }
}
