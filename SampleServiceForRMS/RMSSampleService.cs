using RMS.DSP.UploadDownloadAPI;
using RMS.DSP.UploadDownloadAPI.Entity;
using RMS.DSP.UploadDownloadAPI.Enums;
using Serilog;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Timers;

namespace SampleServiceForRMS
{
    public enum ServiceState
    {
        SERVICE_STOPPED = 0x00000001,
        SERVICE_START_PENDING = 0x00000002,
        SERVICE_STOP_PENDING = 0x00000003,
        SERVICE_RUNNING = 0x00000004,
        SERVICE_CONTINUE_PENDING = 0x00000005,
        SERVICE_PAUSE_PENDING = 0x00000006,
        SERVICE_PAUSED = 0x00000007,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceStatus
    {
        public int dwServiceType;
        public ServiceState dwCurrentState;
        public int dwControlsAccepted;
        public int dwWin32ExitCode;
        public int dwServiceSpecificExitCode;
        public int dwCheckPoint;
        public int dwWaitHint;
    };
    public partial class RMSSampleService : ServiceBase
    {
        private int eventId = 1;

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);

        public RMSSampleService()
        {
            InitializeComponent();

            m_eventLog = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource("RMSSampleService", "RMSSampleServiceLog");
            }
            m_eventLog.Source = "Application";
            //m_eventLog.Log = "RMSSampleServiceLog";

        }

        protected override void OnStart(string[] args)
        {
            ServiceStatus serviceStatus = new ServiceStatus
            {
                dwCurrentState = ServiceState.SERVICE_START_PENDING,
                dwWaitHint = 100000
            };
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            m_eventLog.WriteEntry("In OnStart");



            System.Timers.Timer timer = new System.Timers.Timer
            {
                Interval = 60000 * 1 // 1 minute
            };
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimeUp);
            timer.Start();

            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        private void OnTimeUp(object sender, ElapsedEventArgs e)
        {
            Log.Information("One minute is up");

            RmsClientMasterDataEntity result;
            const string emailId = "";
            const string password = "";
            RmsMaterDataSearchEntity obj1 = new RmsMaterDataSearchEntity()
            {
                EmailId = emailId,
                Password = password,
            };
            RmsResponseEntity responseEntity1 = RmsUploadDownload.GetInstance.GetClientMasterData(obj1, out result);
            if (responseEntity1.Status == ResponseStatus.Success)
            {
                Log.Information(responseEntity1.Message);
                Log.Information(result.ToString());
            }
            else
            {
                Log.Error(responseEntity1.Message);
            }

            RmsUploadEntity obj = new RmsUploadEntity()
            {
                EmailId = emailId,
                Password = password,
                UploadPath = AppDomain.CurrentDomain.BaseDirectory + ".." + "\\.." + "\\.." + "\\030601", 
                AccountName = "",
                AccountType = "Post Bind",
                BusinessUnit = "Property",
                Broker = "Bob",
                ExpireDate = Convert.ToDateTime("2019-02-24"),
                InceptDate = Convert.ToDateTime("2020-02-24"),
                Location = 4,
                PolicyReference = "",
                SpecialInstructions = "",
                SumInsured = 9530756,
                UnderWriter = "Rob",
                WorkType = WorkType.Cleansing,
                WorkFlowReference = "",
                Priority = Priority.Normal
            };

            int AccountId;
            RmsResponseEntity responseEntity = RmsUploadDownload.GetInstance.UploadDSPAccount(obj, out AccountId);
            m_eventLog.WriteEntry(responseEntity.Status.ToString());
            m_eventLog.WriteEntry(responseEntity.Message);
            if (responseEntity.Status == ResponseStatus.Success)
            {
                Log.Information("Account ID : " + AccountId);
                Log.Information(responseEntity.Message);
            }
            else
                Log.Error(responseEntity.Message);
        }

        protected override void OnStop()
        {
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}
