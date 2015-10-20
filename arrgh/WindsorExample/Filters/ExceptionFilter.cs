using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WindsorExample.Filters
{
    public class ExceptionFilter : HandleErrorAttribute
    {
        //for use when extending HandleErrorAttribute
        public override void OnException(ExceptionContext filterContext)
        {
            Log("OnException",filterContext.RouteData, filterContext.Exception);
            filterContext.Result = new ViewResult { ViewName = "Error" };
        }

        //for use when extending FilterAttribute, IExceptionFilter
        /**public void OnException(ExceptionContext filterContext)
        {
            Log("OnException", filterContext.RouteData);
            filterContext.Result = new ViewResult { ViewName = "Error" };
        }**/

        private void Log(string methodName, RouteData routeData, Exception e)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            if (e.Message != null)
            {
                Debug.WriteLine("Action Filter Log: Exception " + e.Message + " in " + message);
            }
            if (e.InnerException != null)
            {
                Debug.WriteLine("Inner Exception Message: " + e.InnerException.Message);
            }
        }
    }
}