namespace SampleServiceForRMS
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rmsSampleServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.rmsSampleServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            this.eventLog1 = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            // 
            // rmsSampleServiceProcessInstaller
            // 
            this.rmsSampleServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.rmsSampleServiceProcessInstaller.Password = null;
            this.rmsSampleServiceProcessInstaller.Username = null;
            // 
            // rmsSampleServiceInstaller
            // 
            this.rmsSampleServiceInstaller.ServiceName = "RMSSampleService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.rmsSampleServiceProcessInstaller,
            this.rmsSampleServiceInstaller});
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller rmsSampleServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller rmsSampleServiceInstaller;
        private System.Diagnostics.EventLog eventLog1;
    }
}