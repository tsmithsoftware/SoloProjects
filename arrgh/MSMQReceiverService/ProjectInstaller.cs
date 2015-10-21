using System.ComponentModel;
using System.ServiceProcess;

namespace MSMQReceiverService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            ServiceProcessInstaller processInstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();
            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = "MCRS MSMQ Receiver Service";
            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }
    }
}
