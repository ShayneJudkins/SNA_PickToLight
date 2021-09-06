using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
//using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SNA.Mobile.PickToLightClient
{
    //Does it need to be a "public class"?
    public class Logger
    //class Logger
    {
        //public enum LogModes { NoLogging, FileLog, EventLog, SQLLog };
        public enum LogModes { NoLogging, FileLog, SQLLog };
        //private LogModes _logMode = LogModes.EventLog;
        private LogModes _logMode = LogModes.NoLogging; //Default to NoLogging (Errors and Main Events will still be logged to the Event Log now)
        private string _logFolder = "";
        private string _appName = "";
        private string _deviceName = "";
        private PickToLightData _pickToLightData;

        public LogModes LogMode
        {
            get
            {
                return _logMode;
            }
            set
            {
                _logMode = value;
            }
        }

        public string LogFolder
        {
            get
            {
                return _logFolder;
            }
            set
            {
                _logFolder = value;
            }
        }

        public Logger(string appName, string deviceName, PickToLightData pickToLightData)
		{
            _appName = appName;
            _deviceName = deviceName;
            _pickToLightData = pickToLightData;
		}

        public void Log(string stringToLog)
        {
            try
            {
                //Where are we logging to?
                if (_logMode.Equals(LogModes.NoLogging))
                {
                    //No Logging, so return!
                    return;
                }
                if (_logMode.Equals(LogModes.FileLog))
                {
                    //Log to File.
                    LogToFile(stringToLog);
                }
                //if (_logMode.Equals(LogModes.EventLog))
                //{
                //    //Log to Event Log
                //    LogToEventLog(stringToLog, false);
                //}
                if (_logMode.Equals(LogModes.SQLLog))
                {
                    //Log to SQL Log Table.
                    LogToSQL(stringToLog);
                }
            }
            catch (Exception e)
            {
                //Log Exceptions to Event Log, no matter what!
                //LogToEventLog("Exception in Logger: " + e.Message, true);
                LogError("Exception in Logger: " + e.Message);
            }
        }

        public void LogToFile(string stringToLog)
        {
            //Make sure the Log Folder extists (this should also be checked when the value is read in)!
            if (Directory.Exists(_logFolder))
            {
                DateTime currentDate = DateTime.Now;
                string fileName = "log" + currentDate.Year.ToString("00") + currentDate.Month.ToString("00") + currentDate.Day.ToString("00") + ".txt";
                string path = _logFolder + (_logFolder[_logFolder.Length - 1] == '\\' ? "" : "\\") + fileName;
                //Log to File.
                //File.AppendAllText(path, "<Thread: " + Thread.CurrentThread.Name + " (ID: " + Thread.CurrentThread.ManagedThreadId.ToString() + ") > " + stringToLog + Environment.NewLine);
                //
                //Use this for Windows CE/Embedded
                //
                using(var writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(stringToLog);
                }
            }

        }

        //public void LogToEventLog(string stringToLog, bool Error)
        //{
        //    using (EventLog eventLog = new EventLog("Application"))
        //    {
        //        string buffer = "<Thread: " + Thread.CurrentThread.Name + " (ID: " + Thread.CurrentThread.ManagedThreadId.ToString() + ") > " + stringToLog;
        //        eventLog.Source = _serviceName; //This will use the name of the Windows Service, which is automatically added as a Source!.
        //        eventLog.Log = "Application";
        //        eventLog.WriteEntry(buffer, (Error ? EventLogEntryType.Error : EventLogEntryType.Information));
        //    }
        //}

        public void LogToSQL(string stringToLog)
        {
            try
            {
                if (_pickToLightData == null) //If we don't have a SQL Connection String yet, crete the Logger with a null PickToLightData instance.
                {
                    return;
                }
                //Create Entry in SQL Log!!!
                //if (LogSQL.CreateLogEntry(_appName, _deviceName, 0, stringToLog) < 0)
                if (_pickToLightData.CreateLogEntry(_appName, _deviceName, 0, stringToLog) < 0)
                {
                    Helpers.ShowError("Error appending to SQL Log Table (" + stringToLog + ")");
                }
            }
            catch (SqlException ex)
            {
                Helpers.ShowError("ERROR appending to SQL Log Table!");
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Helpers.ShowError("Index #" + i +
                        " Message: " + ex.Errors[i].Message +
                        " LineNumber: " + ex.Errors[i].LineNumber +
                        " Source: " + ex.Errors[i].Source +
                        " Procedure: " + ex.Errors[i].Procedure);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    //LogToEventLog("Exception in Logger.LogToSQL() _serviceName (" + _serviceName + "), Thread.CurrentThread.Name (" + Thread.CurrentThread.Name + "), Thread.CurrentThread.ManagedThreadId (" + Thread.CurrentThread.ManagedThreadId + "), stringToLog(" + stringToLog + "). Exception: " + ex.Message + " (Inner Exception: " + ex.InnerException + ")", true);
                    Helpers.ShowError("Exception in Logger.LogToSQL() stringToLog(" + stringToLog + "). Exception: " + ex.Message + " (Inner Exception: " + ex.InnerException + ")");
                }
                else
                {
                    //LogToEventLog("Exception in Logger.LogToSQL() _serviceName (" + _serviceName + "), Thread.CurrentThread.Name (" + Thread.CurrentThread.Name + "), Thread.CurrentThread.ManagedThreadId (" + Thread.CurrentThread.ManagedThreadId + "), stringToLog(" + stringToLog + "). Exception: " + ex.Message, true);
                    Helpers.ShowError("Exception in Logger.LogToSQL() stringToLog(" + stringToLog + "). Exception: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Called when a "Main Event" occurs.
        /// For instance:
        ///     The Service is Started or Stopped.
        ///     A Thread is Started or Stopped.
        ///     What Settings were loaded (when the service starts up).
        ///Adds an "Information" entry to the Event Log and creates an entry using whatever LogMode is being used.
        /// </summary>
        /// <param name="stringToLog"></param>
        //public void LogMainEvent(string stringToLog)
        //{
        //    //Called when service/thread is Started or Stoped, settings are loaded (LoadSettings()) OR ??
        //    //
        //    //Call LogToEventLog() as "Information" AND Log() to use whatever LogMode is set!
        //    LogToEventLog(stringToLog, false);
        //    if (this.LogMode != LogModes.EventLog)  //Prevent logging to the Event log twice!
        //    {
        //        Log(stringToLog);
        //    }
        //}

        /// <summary>
        /// Called when an error is encountered.
        /// For instance:
        ///     When an exception is caught in Try/Catch block.
        ///     A badly formatted command is recceived in the socket protocol
        ///     Error reading data from scanner.
        ///     Etc.
        /// Adds an Error entry to the Log, using whatever LogMode is being used.
        /// Also shows the Error to the user, using a PopUp type Dialog.
        /// </summary>
        /// <param name="stringToLog"></param>
        public void LogError(string stringToLog)
        {
            //LogToEventLog(stringToLog, true);
            //if (this.LogMode != LogModes.EventLog)  //Prevent logging to the Event log twice!
            //{
            //    Log(stringToLog);
            //}
            Log(stringToLog);
            Helpers.ShowError(stringToLog);
        }
    }
}
