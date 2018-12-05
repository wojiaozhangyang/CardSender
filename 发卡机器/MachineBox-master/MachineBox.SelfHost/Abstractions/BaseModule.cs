using Nancy;
using Nancy.Json;
using System;

namespace MachineBox.SelfHost.Abstractions
{
    public abstract class BaseModule : NancyModule
    {
        public BaseModule(params string[] urlArray)
        {
            JsonSettings.RetainCasing = true;

            foreach (var url in urlArray)
            {
                Options[url] = parameters =>
                {
                    return Response.AsJson(new { }).WithHeaders(new Tuple<string, string>[] {
                        new Tuple<string, string>("Access-Control-Allow-Origin", "*"),
                        new Tuple<string, string>("Access-Control-Allow-Methods", "OPTIONS, GET, POST, PUT, DELETE"),
                        new Tuple<string, string>("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Access-Control-Allow-Origin, Access-Control-Allow-Headers, Access-Control-Allow-Methods")
                    });
                };
            };
        }
    }
}