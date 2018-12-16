using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Http.Dependencies;
using Data;
using Data.Implementations.Context;
using Data.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using Services;
using Services.Interfaces;
using WebUI.Identity;
using IDependencyResolverMVC = System.Web.Mvc.IDependencyResolver;

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolverMVC, System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings(_kernel);
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(this._kernel.BeginBlock());
        }

        public static void AddBindings(IKernel _kernel)
        {
            var appName = "Application";


            _kernel.Bind<DataContext>().To<DataContext>().InThreadScope();
            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InThreadScope();           
            _kernel.Bind<ApplicationUserManager>().ToSelf().InRequestScope();
            _kernel.Bind<ApplicationRoleManager>().ToSelf().InRequestScope();
            _kernel.Bind<ApplicationSignInManager>().ToSelf().InRequestScope();

            ApplyServicesDependencies(_kernel);

            _kernel.Bind<IIdentityMessageService>().To<EmailService>().InRequestScope();
            _kernel.Bind<IDataSerializer<AuthenticationTicket>>().To<TicketSerializer>();
            _kernel.Bind<ISecureDataFormat<AuthenticationTicket>>().To<SecureDataFormat<AuthenticationTicket>>();
            _kernel.Bind<ITextEncoder>().To<Base64UrlTextEncoder>();
            _kernel.Bind<IAuthenticationManager>()
                .ToMethod(c => HttpContext.Current.GetOwinContext().Authentication)
                .InRequestScope();

        }


        private static void ApplyServicesDependencies(IKernel _kernel)
        {
            var builders = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IDependencyBuilder).IsAssignableFrom(p) && !p.IsInterface);
            foreach(var builder in builders)
            {
                var instance = (IDependencyBuilder)Activator.CreateInstance(builder);
                instance.ApplyBindings(_kernel);
            }
        }

        public void Dispose()
        {
            _kernel?.Dispose();
        }
    }
}