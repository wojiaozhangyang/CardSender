using MachineBox.Core.CardReaders;
using MachineBox.Core.Models;
using MachineBox.SelfHost.Abstractions;
using Nancy;
using Nancy.Json;
using System;

namespace MachineBox.SelfHost.Modules
{
    public class CRT602UModule : BaseModule
    {
        public CRT602UModule() : base("/api/crt602u/read", "/api/crt602u/check")
        {
            Get["/api/crt602u/read"] = parameters =>
            {
                var response = new CRT602UReader().Read();

                return Response.AsJson(new ApiResponse<string> {
                    Status  = (int)response.Status,
                    Data    = response.Data
                }).WithHeader("Access-Control-Allow-Origin", "*");
            };

            Get["/api/crt602u/check"] = parameters =>
            {
                var response = new CRT602UReader().IsConnected();

                return Response.AsJson(new ApiResponse
                {
                    Status  = (int)response.Status
                }).WithHeader("Access-Control-Allow-Origin", "*");
            };
        }
    }
}
