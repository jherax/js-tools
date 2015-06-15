using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TwitterService.Infrastructure {

    /// <summary>
    /// Writes an exception to a file
    /// </summary>
    /// <remarks>
    /// Lazy sigleton pattern
    /// </remarks>
    /// <!-- Created by: David Rivera -->
    internal sealed class Logger {

        #region Private Members

        /// <summary>
        /// The private constructor does not allow a default constructor be generated,
        /// and thus avoids direct instantiation.
        /// </summary>
        private Logger() {
            this.LogFile = AppDomain.CurrentDomain.BaseDirectory + @"ErrorLog.txt";
        }

        /// <summary>
        /// Creates the instance when accessed the first time
        /// </summary>
        /// <see cref="http://msdn.microsoft.com/en-us/library/dd642329(v=vs.110).aspx"/>
        private static readonly Lazy<Logger> instance = new Lazy<Logger>(() => new Logger());

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the exception and builds the message
        /// </summary>
        /// <param name="exc">Excepción</param>
        private string BuildTrace(Exception exc) {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("********** {0} **********", DateTime.Now);
            sb.AppendLine();
            // Gets the original exception
            if (exc.InnerException != null) {
                sb.Append("Inner Exception Type: ");
                sb.AppendLine(exc.InnerException.GetType().ToString());
                sb.AppendLine("Inner Exception: " + exc.InnerException.Message);
                sb.AppendLine("Inner Source: " + exc.InnerException.Source);
                sb.AppendLine("Inner Stack Trace: ");
                sb.AppendLine(exc.InnerException.StackTrace ?? "");
            }
            // Registers the current exception
            sb.Append("Exception Type: ");
            sb.AppendLine(exc.GetType().ToString());
            sb.AppendLine("Exception: " + exc.Message);
            sb.AppendLine("Stack Trace: ");
            sb.AppendLine(exc.StackTrace ?? "");
            sb.AppendLine();

            return sb.ToString();
        } //end method

        /// <summary>
        /// Escribe en el archivo
        /// </summary>
        /// <param name="text">Texto a escribir</param>
        /// <param name="filepath">Ruta del archivo</param>
        private void WriteFile(string text, string filepath) {
            byte[] encoded = Encoding.Unicode.GetBytes(text);
            using (FileStream fs = new FileStream(
                path: filepath,
                mode: FileMode.Append,
                access: FileAccess.Write,
                share: FileShare.Read,
                bufferSize: 4096)) {
                fs.Write(encoded, 0, encoded.Length);
            } //end using
        }
        #endregion

        /// <summary>
        /// Property to access the instance
        /// </summary>
        public static Logger GetInstance {
            get {
                return instance.Value;
            }
        }

        /// <summary>
        /// Sets the log file path
        /// </summary>
        public string LogFile { get; set; }

        /// <summary>
        /// Writes the exception in the file
        /// </summary>
        /// <param name="exc">Excepción</param>
        public void LogException(Exception exc) {
            WriteFile(BuildTrace(exc), this.LogFile);
        }

    }//end class
}//end namespace
