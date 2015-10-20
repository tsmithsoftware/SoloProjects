using Castle.DynamicProxy;
using NLog;
using System.Diagnostics;
using System;
using WindsorExample.Controllers;

namespace WindsorExample.Filters
{
    public class LoggingAspect : IInterceptor
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public void Intercept(IInvocation invocation)
        {
            try
            {
                WriteLine(invocation.TargetType.FullName + " | Entering method " +
                          invocation.Method.Name, false);

                WriteLine(string.Format("After method: {0}", invocation.Method.Name),false);
                invocation.Proceed();
            }
            catch (Exception e)
            {
                WriteLine("new page would be returned here" +e.Message, true);
            }
            
        }

        private void WriteLine(string v, bool exception)
        {
            Debug.WriteLine(string.Format(v));
            if (!exception)
            {
                logger.Debug(string.Format(v));
            }
            else
            {
                logger.Error(v);
            }
        }
    }
}