using MachineBox.Core.CardReaders;
using MachineBox.Core.Models;
using MachineBox.SelfHost.Abstractions;
using MachineBox.Win.Helpers;
using Nancy;

namespace MachineBox.SelfHost.Modules
{
    public class USBHIDModule : BaseModule
    {
        public USBHIDModule(): base("/api/usbhid/read", "/api/usbhid/barcoderead")
        {
            Get["/api/usbhid/read"] = parameters =>
            {
                var response = new USBHIDReader().Read(false);

                return Response.AsJson(new ApiResponse<string> {
                    Status  = (int)response.Status,
                    Message = response.Status.ToString(),
                    Data    = response.Data
                }).WithHeader("Access-Control-Allow-Origin", "*");
            };

            Get["/api/usbhid/barcoderead"] = parameters =>
            {
                var response = new USBHIDReader().Read(true);

                if(Settings.Get(Settings.Keys.RemoveLastCharacterFromScannedBarcode) == "1")
                {
                    response.Data = response.Data.Length > 0 ? response.Data.Remove(response.Data.Length - 1) : response.Data;
                }

                return Response.AsJson(new ApiResponse<string>
                {
                    Status = (int)response.Status,
                    Message = response.Status.ToString(),
                    Data = response.Data
                }).WithHeader("Access-Control-Allow-Origin", "*");
            };
        }
    }
}
