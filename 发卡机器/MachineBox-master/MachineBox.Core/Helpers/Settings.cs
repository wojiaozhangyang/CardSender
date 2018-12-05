using System;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace MachineBox.Win.Helpers
{
    public static class Settings
    {
        /// <summary>
        /// 
        /// </summary>
        public struct Keys
        {
            /// <summary>
            /// 
            /// </summary>
            public static string RemoveLastCharacterFromScannedBarcode = "RemoveLastCharacterFromScannedBarcode";

            /// <summary>
            /// 
            /// </summary>
            public static string USBHIDCompabilityMode                 = "USBHIDCompabilityMode";
        }

        /// <summary>
        /// 
        /// </summary>
        private static string _directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Application.ProductName);

        /// <summary>
        /// 
        /// </summary>
        private static string _filePath      = Path.Combine(_directoryPath, "Settings.mbox");

        /// <summary>
        /// 
        /// </summary>
        static Settings()
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
        public static void Add(string key, string value, bool overwrite = true)
        {
            var arrLine = File.ReadAllLines(_filePath).ToList();

            var exists = false;

            for(var i = 0; i < arrLine.Count; i++)
            {
                var s_line = arrLine[i].Trim().Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

                var t_key = s_line[0];
                var t_val = s_line[1];

                if(key == t_key)
                {
                    exists = !exists;

                    if (overwrite)
                    {
                        arrLine[i] = $"{key}={value}";
                    }

                    break;
                }
            }

            if (!exists)
            {
                arrLine.Add($"{key}={value}");
            }

            File.WriteAllLines(_filePath, arrLine);
         }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Get(string key)
        {
            var arrLine = File.ReadAllLines(_filePath).ToList();

            for (var i = 0; i < arrLine.Count; i++)
            {
                var s_line = arrLine[i].Trim().Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

                var t_key = s_line[0];
                var t_val = s_line[1];

                if (key == t_key)
                {
                    return t_val;
                }
            }

            return default(string);
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
