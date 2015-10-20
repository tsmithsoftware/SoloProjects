using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WindsorExample.Controllers;
using WindsorExample.Filters;
using WindsorExample.Plumbing;

namespace WindsorExample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /**What we're missing is to actually create our container we'll be using in the app (the one and only instance), install our installer, and tell MVC infrastructure to use our controller factory instead of its own default. All of that happens in the global.asax file.
        To do this let's add the following code, and invoke BootstrapContainer method at the end of Application_Start:**/
        private static IWindsorContainer container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BootstrapContainer();
        }

        /**We're instantiating the WindsorContainer class which is the core class in Windsor (as its name implies). We then call its Install method. Reading it from inside out, the FromAssembly class will look for, instantiate and return all installers in our assembly (this means our sole ControllersInstaller for now, but in the future we'll have more). Then WindsorContainer will call down to each of those installers, which in turn will register the components specified by each installer.
        We then create our WindsorControllerFactory passing it the Kernel wrapped by the container, and we attach the factory to MVC infrastructure.**/
        private static void BootstrapContainer()
        {
            container = new WindsorContainer().Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            container.Register(Component.For<LoggingAspect>());
        }

        //Finally, we need to clean up when the application ends(remember - clean up is as important as creation).
        protected void Application_End()
        {
            container.Dispose();
        }
    }
}
