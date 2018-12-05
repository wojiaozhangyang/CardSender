using Nancy.Hosting.Self;
using System;

namespace MachineBox.SelfHost
{
    public static class NancySelfHost
    {
        private static NancyHost _nancyHost;

        /// <summary>
        /// Start listening for incoming requests with the given configuration
        /// </summary>
        public static void Start(int port)
        {
            Stop();

            _nancyHost = new NancyHost(new HostConfiguration() { RewriteLocalhost = true }, new Uri($"http://localhost:{port}"));
            _nancyHost.Start();
        }

        /// <summary>
        /// Stop listening for incoming requests.
        /// </summary>
        public static void Stop()
        {
            if (_nancyHost != null)
            {
                _nancyHost.Stop();
            }
        }
    }
}
