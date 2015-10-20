using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WindsorExample.Filters;

namespace WindsorExample
{
    /**
    Our installer needs to tell Windsor two things: how to find controllers in our app, and how to configure them.

The first part - finding controllers - is easy, as ASP.NET MVC requires by convention, that controllers implement IController interface.

Configuration for now will be pretty simple too. First of all, MVC framework requires that we create a new controller instance each time it asks us for one. This is different from Windsor's default which would create just one instance the first time we ask for one, and then reuse it for all subsequent requests. The installer that meets the requirements may look like this:
    **/
    public class ControllersInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IController>()
                .LifestyleTransient()
                );
                //.Configure(r => r.Interceptors<LoggingAspect>())); //incorporate to use interceptors
        }
    }
    /**
    The installer uses the container parameter of the Install method to Register controllers using Windsor's Fluent Registration API.
    Every time we add a new controller type into our application (and big apps can have hundreds of them) it will be automatically registered.
    Let's quickly go over what the code does. Classes static class is the entry point to the registration and it will look for public, non-abstract classes         FromThisAssembly, that is the assembly containing the installer (that is our MVC assembly, which is where, indeed, the controllers live).
    We don't want just any type from the assembly though. The BasedOn<IController> filters the classes even further to just those that implement IController.       The method is named BasedOn rather than Implements because it can be used for base classes just as well as interfaces, and it even handle open generic   types (but let's not worry about that just yet).
    The last part controls a very important aspect of working with Windsor - instance lifestyle. Transient is what MVC framework expects that is, a new instance     should be provided by Windsor every time it is needed, and it's the caller's responsibility to tell Windsor when that instance is no longer needed         and can be discarded (That's what the ReleaseController method on our controller factory is for).
    **/
}
