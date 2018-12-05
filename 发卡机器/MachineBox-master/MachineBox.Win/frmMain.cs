using System;
using System.Collections.Generic;
using System.Configuration;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MachineBox.Core.Globals;
using MachineBox.Core.Helpers;
using MachineBox.SelfHost;
using MachineBox.Win.Helpers;

namespace MachineBox.Win
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private string _site = "http://dmmdvincdbt1:4000";

        /// <summary>
        /// 
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            LoadSettings();

            StartServer();

            Hook();

            TurnOnAutoUpdate();

            AddShortcut();

            StartKiosk();

            Text = $"MachineBox v{GetRunningVersion().ToString()}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Version GetRunningVersion()
        {
            try
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool StartServer()
        {
            try
            {
                Print("Server is starting..");

                var port = int.Parse(ConfigurationManager.AppSettings["port"]);

                NancySelfHost.Start(port);

                Print($"Server started successfully");

                Print($"Listening on http://localhost:{port}");

                return true;
            }
            catch (Exception e)
            {
                Print(e.Message);

                Logger.Log(e);

                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool Hook()
        {
            try
            {
                Print("Hooking keyboard...");

                USBHIDGlobal.KeyboardHook.KeyDown += (sender, e) =>
                {
                    if (!USBHIDGlobal.Wait)
                        if (e == USBHIDGlobal.END_CHAR)
                        {
                            if (Settings.Get(Settings.Keys.USBHIDCompabilityMode) == "1" && !USBHIDGlobal.BypassCompabilityMode)
                            {
                                long quotient = long.Parse(USBHIDGlobal.Text),
                                     temp = 0;
                                int i = 0;

                                List<char> hexadecimalNumber = new List<char>();

                                while (quotient != 0)
                                {
                                    temp = quotient % 16;

                                    if (temp < 10)
                                    {
                                        temp = temp + 48;
                                    }
                                    else
                                    {
                                        temp = temp + 55;
                                    }

                                    var ch = Convert.ToChar(temp);

                                    if ((++i) % 2 == 0)
                                    {
                                        hexadecimalNumber.Insert(hexadecimalNumber.Count - 1, ch);
                                    }
                                    else
                                    {
                                        hexadecimalNumber.Add(ch);
                                    }

                                    quotient = quotient / 16;
                                }

                                USBHIDGlobal.Text = new string(hexadecimalNumber.ToArray());

                            }

                            USBHIDGlobal.Wait = true;
                        }
                        else
                            USBHIDGlobal.Text += (char)e;
                };

                Print("Hooked successfully");

                return true;
            }
            catch (Exception e)
            {
                Print(e.Message);

                Logger.Log(e);

                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void TurnOnAutoUpdate()
        {
            Print("Auto-update turned on");

            tmrUpdate.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddShortcut()
        {
            try
            {
                string shortcutFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), Application.ProductName + ".appref-ms");
                string publisherName    = Application.ProductName;
                string productName      = Application.ProductName;
                string allProgramsPath  = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
                string shortcutPath     = Path.Combine(Path.Combine(allProgramsPath, publisherName), productName) + ".appref-ms";

                System.IO.File.Copy(shortcutPath, shortcutFileName, true);

                Print("Shortcut added successfully");
            }
            catch (Exception e)
            {
                Print(e.Message);

                Logger.Log(e);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void Print(string value)
        {
            txtSummary.AppendText(String.Format("{0:dd.MM.yyyy HH:mm:ss} - {1}{2}", DateTime.Now, value, Environment.NewLine));
        }

        /// <summary>
        /// 
        /// </summary>
        private void StartKiosk()
        {
            Process.Start("cmd", "/c start chrome --kiosk " + _site);
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadSettings()
        {
            chckCompabilityModeBarcodeReader.Checked = Settings.Get(Settings.Keys.USBHIDCompabilityMode                      ) == "1";
            chckRemoveLastCharacter         .Checked = Settings.Get(Settings.Keys.RemoveLastCharacterFromScannedBarcode      ) == "1";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            var info = default(UpdateCheckInfo);

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                var ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                    if (info.UpdateAvailable)
                    {
                        Print("New version " + info.AvailableVersion + " is available");

                        Print("Installing...");

                        if (ad.Update())
                        {
                            Print("Successfully updated");

                            Print("Restarting...");

                            Application.Restart();
                        }
                        else
                            Print("Update failed");
                    }
                }
                catch (Exception ex)
                {
                    Print(ex.Message);

                    Logger.Log(ex);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenLogs_Click(object sender, EventArgs e)
        {
            Process.Start(Logger.DirectoryPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartKiosk_Click(object sender, EventArgs e)
        {
            StartKiosk();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chckCompabilityModeBarcodeReader_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Add(Settings.Keys.USBHIDCompabilityMode, (sender as CheckBox).CheckState == CheckState.Checked ? "1" : "0");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chckRemoveLastCharacter_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Add(Settings.Keys.RemoveLastCharacterFromScannedBarcode, (sender as CheckBox).CheckState == CheckState.Checked ? "1" : "0");
        }
    }
}
