using System;
using System.Diagnostics;

public class CronJob
{
    // The state object is necessary for a TimerCallback.
    public void DoSomething(object stateObject)
    {
        Trace.WriteLine("Job Fired!" + DateTime.Now);
    }
}