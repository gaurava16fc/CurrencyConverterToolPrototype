using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterTool.ExceptionLogger
{
    public class ExceptionLogging
    {
        private readonly string _exceptionSource = "";
        public ExceptionLogging(string exceptionSource)
        {
            _exceptionSource = exceptionSource;
        }

        /// <summary>
        /// Trace the Exception
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>Returns cumputed ExceptionID after logging the exception</returns>
        private string ExceptionTrace(Exception exception)
        {
            Guid HandlingInstanceId = System.Guid.NewGuid();

            string message = Environment.NewLine + Environment.NewLine;
            string ExceptionID = HandlingInstanceId.ToString() + Environment.NewLine + Environment.NewLine;
            message += "ExceptionID: " + HandlingInstanceId.ToString() + Environment.NewLine + Environment.NewLine;
            message += "Detail: " + exception.Message + Environment.NewLine;

            if (exception.InnerException != null && !String.IsNullOrEmpty(exception.InnerException.Message))
            {
                message += "Inner Exception : " + exception.InnerException.Message + Environment.NewLine;
            }

            message += "StackTrace: " + exception.StackTrace + Environment.NewLine;

            if (exception.InnerException != null)
            {
                message += "Inner Exception : " + exception.InnerException.Message + Environment.NewLine;
            }

            Exception newEx = new Exception(message);
            //ExceptionPolicy.HandleException(newEx , "ExceptionPolicy");
            WriteToEventLog(message, EventLogEntryType.Error);
            return ExceptionID;
        }

        /// <summary>
        /// Log exception into event viewer
        /// </summary>
        /// <param name="strLogEntry"></param>
        /// <param name="eType"></param>
        /// <param name="exceptionSource"></param>
        private void WriteToEventLog(string strLogEntry, EventLogEntryType eType)
        {
            try
            {
                string strSource = _exceptionSource; //name of the source

                /*TODO# UNCOMMENT THE BELOW REGION WHEN YOU ARE RUNNING THE APPLICATION FOR THE VERY FIRST TIME... 
                 * IT WILL BE THROWING SECURITY ISSUES EXCEPTION...
                 * When it creates the Event Log Source at Event Viewer, then do comment out this below region
                 * */
                #region Code to create Event Viewer Log Source..
                //string strLogType = "CurrencyConverterToolLog"; //type of the log

                //if (!EventLog.SourceExists(strSource))
                //{
                //    EventLog.CreateEventSource(strSource, strLogType);
                //}
                #endregion

                EventLog eLog = new EventLog();
                eLog.Source = strSource;
                eLog.WriteEntry(strLogEntry, eType);
            }
            catch (Exception ex)
            { }
        }

        private void DisplayExceptionToConsole(string exceptionID, Exception ex)
        {
            if (string.IsNullOrEmpty(exceptionID) == false && ex != null)
            {
                string errorFound = ex.Message + " Please contact to administrator with exceptionID : " + exceptionID;
                if (ex.InnerException != null && !String.IsNullOrEmpty(ex.InnerException.Message))
                    errorFound += "\nInner Exception ==> " + ex.InnerException.Message;
                if (!string.IsNullOrEmpty(errorFound))
                    Console.WriteLine("Exception: \n" + errorFound);
            }
        }



        /// <summary>
        /// Trace/Log the Exception and also displays the exception to the console
        /// </summary>
        /// <param name="exception"></param>
        public void LogException(Exception exception)
        {
            string exceptionID = "No Data Found";
            exceptionID = ExceptionTrace(exception);
            DisplayExceptionToConsole(exceptionID, exception);
        }
    }

}
