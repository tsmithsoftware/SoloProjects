﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;

namespace InstallingCronTask
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Get the service on the local machine
            //below doesnt work
            //ended up using code in text file
            /** using (TaskService ts = new TaskService())
             {
                 // Create a new task definition and assign properties
                 TaskDefinition td = ts.NewTask();
                 td.RegistrationInfo.Description = "MCRS test job Email Notifications";

                 // Create a trigger that will fire the task at this time every other day
                 td.Triggers.Add(new DailyTrigger { DaysInterval = 2 });

                 // Create an action that will launch Notepad whenever the trigger fires
                 td.Actions.Add(new ExecAction("notepad.exe", "c:\\test.log", null));

                 // Register the task in the root folder
                 ts.RootFolder.RegisterTaskDefinition(@"Test", td);
             }**/
            Console.WriteLine("New Job Fired from Aargh Project.");
            Console.ReadLine();
        }
    }
}
