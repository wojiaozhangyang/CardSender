using System;
using System.IO;
using System.Windows.Forms;
using MachineBox.Core.Models;
using Newtonsoft.Json;

namespace MachineBox.Core.Helpers
{
    public static class Logger
    {
        /// <summary>
        /// 
        /// </summary>
        private static string _directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Path.Combine(Application.ProductName,"Logs"));

        /// <summary>
        /// 
        /// </summary>
        private static string _filePath      = Path.Combine(_directoryPath, string.Format("Log-{0:yyyy-MM-dd}.mbox", DateTime.Now));

        /// <summary>
        /// 
        /// </summary>
        public static string DirectoryPath
        {
            get
            {
                return _directoryPath;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static Logger()
        {
            Create();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public static void Log(Exception ex)
        {
            var err = new ErrorLog
            {
                ErrorMessage          = ex.Message,
                Source                = ex.Source,
                StackTrace            = ex.StackTrace,
                Target                = ex.TargetSite ?.ToString() ?? string.Empty,
                InnerExceptionMessage = ex.InnerException?.Message ?? string.Empty
            };

            using (var sw = File.AppendText(_filePath))
            {
                sw.WriteLine(string.Format("{0:dd/MM/yyyy HH:mm:ss} : {1}", DateTime.Now, JsonConvert.SerializeObject(err)));

                sw.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void Create()
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            if (!File.Exists(_filePath))
            {
                using (var fIn = File.Create(_filePath))
                {
                    fIn.Close();
                }
            }
        }
    }
}
