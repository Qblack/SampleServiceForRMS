using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SampleServiceForRMS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "logs\\log-.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information("Starting up");
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new RMSSampleService()
            };

            ServiceBase.Run(ServicesToRun);
        }
    }
}
