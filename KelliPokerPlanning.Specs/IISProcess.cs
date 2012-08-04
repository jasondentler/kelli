using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace KelliPokerPlanning.Specs
{

    public class IISProcess : IDisposable
    {

        private const string IISExpressPathKey = "IISExpressPath";

        private readonly string _websiteLocation;
        private readonly int? _portNumber;
        private readonly string _iisExpressPath;
        private Process _iisProcess;

        public IISProcess() : this(GetWebsitePath(), null, GetIISExpressPath())
        {
        }

        public IISProcess(int portNumber) : this(GetWebsitePath(), portNumber, GetIISExpressPath())
        {
        }

        public IISProcess(string websiteLocation) : this(websiteLocation, null, GetIISExpressPath())
        {
            _websiteLocation = websiteLocation;
        }

        public IISProcess(string websiteLocation, int portNumber) : this(websiteLocation, portNumber, GetIISExpressPath())
        {
        }

        private IISProcess(string websiteLocation, int? portNumber, string iisExpressPath)
        {
            _websiteLocation = websiteLocation;
            _portNumber = portNumber;
            _iisExpressPath = iisExpressPath;
        }

        private static string GetWebsitePath()
        {
            return Settings.WebsitePath;
        }

        private static string GetIISExpressPath()
        {
            return Settings.IISExpressPath;
        }

        public void Start()
        {
            var args = new Dictionary<string, string>()
                           {
                               {"path", string.Format("\"{0}\"", _websiteLocation)}
                           };

            if (_portNumber.HasValue)
                args["port"] = _portNumber.Value.ToString(CultureInfo.InvariantCulture);

            var arguments = string.Join(" ", args.Select(x => string.Format("/{0}:{1}", x.Key, x.Value)));

            var si = new ProcessStartInfo(_iisExpressPath, arguments)
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            _iisProcess = new Process() { StartInfo = si };
            _iisProcess.Start();
            WaitForStartup(_iisProcess);
        }

        public void Stop()
        {
            if (_iisProcess == null || _iisProcess.HasExited) return;

            _iisProcess.Kill();

            _iisProcess.WaitForExit(Convert.ToInt32(TimeSpan.FromSeconds(10).TotalMilliseconds));
        }

        private static void WaitForStartup(Process process)
        {
            const string waitForText = "IIS Express is running.";
            var output = new StringBuilder();
            var outputBuffer = new char[1000];
            var stdout = process.StandardOutput;
            Console.WriteLine("Waiting for IIS Express to start up.");

            var hasStarted = new Func<bool>(() => output.ToString().Contains(waitForText));

            while (!hasStarted() && !process.HasExited)
            {
                var outputLength = outputBuffer.Length;
                while (!hasStarted() && !process.HasExited && outputLength == outputBuffer.Length)
                {
                    outputLength = stdout.Read(outputBuffer, 0, outputBuffer.Length);
                    output.Append(outputBuffer, 0, outputLength);
                    Console.Write(outputBuffer, 0, outputLength);
                }
            }
        }

        public void Dispose()
        {
            Stop();
        }

    }
}