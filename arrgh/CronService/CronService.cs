using System.ServiceProcess;
using System.Threading;

namespace AG
{
    /**A Service which writes to a text file every second**/
    public class CronService : ServiceBase
    {
        private CronJob job;
        private Timer stateTimer;
        private TimerCallback timerDelegate;

        public CronService()
        {
            this.ServiceName = "Cron";
            this.CanStop = true;
            this.CanPauseAndContinue = false;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            //Below code fires once a second
            job = new CronJob();
            timerDelegate = new TimerCallback(job.DoSomething);
            stateTimer = new Timer(timerDelegate, null, 1000, 1000);
        }

        protected override void OnStop()
        {
            // do shutdown stuff
            stateTimer.Dispose();
        }

        public static void Main()
        {
            Run(new CronService());
        }
    }
}
