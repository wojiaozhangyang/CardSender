using MachineBox.Core.Enums;
using MachineBox.Core.Globals;
using MachineBox.Core.Hooks;
using MachineBox.Core.Models;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace MachineBox.Core.CardReaders
{
    public class USBHIDReader
    {
        public CardReaderResponse Read(bool bypassCompabilityMode = false)
        {
            var response = new CardReaderResponse { Status = ResponseStatuses.FAILURE };
            var start    = DateTime.Now;

            USBHIDGlobal.Text                  = string.Empty;
            USBHIDGlobal.BypassCompabilityMode = bypassCompabilityMode;
            USBHIDGlobal.Wait                  = false;
            

            while (true)
            {
                if ((DateTime.Now - start).TotalSeconds >= int.Parse(ConfigurationManager.AppSettings["readTimeout"]))
                {
                    response.Status = ResponseStatuses.TIMEOUT_EXPIRED;
                    break;
                }

                if (USBHIDGlobal.Wait)
                {
                    response.Status = ResponseStatuses.SUCCESS;
                    response.Data   = USBHIDGlobal.Text;
                    break;
                }
            }

            USBHIDGlobal.Wait                  = true;
            USBHIDGlobal.BypassCompabilityMode = bypassCompabilityMode;

            return response;
        }

    }
}