using Castle.Windsor;
using System.Web.Mvc;
using Xunit;
using System;
using Castle.MicroKernel;
using System.Linq;
using Castle.Core.Internal;
using Castle.Core;

namespace WindsorExample.Tests
{
    public class ControllerInstanceTests
    {
        private IWindsorContainer containerWithControllers;

        public ControllerInstanceTests()
        {
            containerWithControllers = new WindsorContainer()
                .Install(new ControllersInstaller());
        }


        // verifying the first rule - that all types implementing IController are registered and that only types implementing IController are registered
        [Fact]
        public void All_controllers_implement_IController()
        {
            var allHandlers = GetAllHandlers(containerWithControllers);
            var controllerHandlers = GetHandlersFor(typeof(IController), containerWithControllers);

           // Assert.NotEmpty(allHandlers);
           // Assert.Equal(allHandlers, controllerHandlers);
        }
        /**
        GetAllHandlers and GetHandlersFor are helper methods for our test that return all Handlers from the container, and all handlers where implementation type is assignable to a given type, respectively.**/
        private object GetAllHandlers(IWindsorContainer containerWithControllers)
        {
            return GetHandlersFor(typeof(object), containerWithControllers);
        }

        private IHandler[] GetHandlersFor(Type type, IWindsorContainer container)
        {
            return container.Kernel.GetAssignableHandlers(type);
        }

        /**This test only validates the first part of our assumption. Let's now write a test for the second part - to verify that indeed all types implementing IController from our application assembly are registered by the ControllersInstaller**/
        [Fact]
        public void All_controllers_are_registered()
        {
            // Is<TType> is an helper, extension method from Windsor in the Castle.Core.Internal namespace
            // which behaves like 'is' keyword in C# but at a Type, not instance level
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Is<IController>());
            var registeredControllers = GetImplementationTypesFor(typeof(IController), containerWithControllers);
            Assert.Equal(allControllers, registeredControllers);
        }

        //We're finding all controller classes in the assembly, and getting implementation types of each component registered and making sure the sets are equal.

        private Type[] GetImplementationTypesFor(Type type, IWindsorContainer container)
        {
            return GetHandlersFor(type, container)
                .Select(h => h.ComponentModel.Implementation)
                .OrderBy(t => t.Name)
                .ToArray();
        }

        private Type[] GetPublicClassesFromApplicationAssembly(Predicate<Type> where)
        {
            return typeof(Controllers.HomeController).Assembly.GetExportedTypes()
                .Where(t => t.IsClass)
                .Where(t => t.IsAbstract == false)
                .Where(where.Invoke)
                .OrderBy(t => t.Name)
                .ToArray();
        }

        /**
        While the installer only requires controllers to implement IController to work correctly, a good practice is to be more demanding. With default routing rules in ASP.NET MVC controller classes should each have Controller suffix. Also the default structure of the project puts them all in a common namespace.
        Neither of those can be validated by the compiler. However we can easily use container and our existing test code the enforce those rules.**/

        [Fact]
        public void All_and_only_controllers_have_Controllers_suffix()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Name.EndsWith("Controller"));
            var registeredControllers = GetImplementationTypesFor(typeof(IController), containerWithControllers);
            Assert.Equal(allControllers, registeredControllers);
        }

        [Fact]
        public void All_and_only_controllers_live_in_Controllers_namespace()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Namespace.Contains("Controllers"));
            var registeredControllers = GetImplementationTypesFor(typeof(IController), containerWithControllers);
            Assert.Equal(allControllers, registeredControllers);
        }

        /**
        Having this done and working it's time to verify that we configured the types the way we wanted to.**/
        [Fact]
        public void All_controllers_are_transient()
        {
            var nonTransientControllers = GetHandlersFor(typeof(IController), containerWithControllers)
                .Where(controller => controller.ComponentModel.LifestyleType != LifestyleType.Transient)
                .ToArray();

            Assert.Empty(nonTransientControllers);
        }

        [Fact]
        public void All_controllers_expose_themselves_as_service()
        {
            var controllersWithWrongName = GetHandlersFor(typeof(IController), containerWithControllers)
                .Where(controller => controller.ComponentModel.Services.Single() != controller.ComponentModel.Implementation)
                .ToArray();

            Assert.Empty(controllersWithWrongName);
        }
    }
}
 
 
 
 
 