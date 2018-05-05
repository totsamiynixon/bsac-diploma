using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Http.Dependencies;
using Data;
using Data.Context;
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
using WebUI.Identity;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
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
        private void AddBindings()
        {
            var appName = "Application";


            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();           
            _kernel.Bind<DataContext>().To<DataContext>().InRequestScope();
            _kernel.Bind<ApplicationUserManager>().ToSelf().InRequestScope();
            _kernel.Bind<ApplicationRoleManager>().ToSelf().InRequestScope();
            _kernel.Bind<ApplicationSignInManager>().ToSelf().InRequestScope();

            _kernel.Bind<IIdentityMessageService>().To<EmailService>().InRequestScope();
            _kernel.Bind<IDataSerializer<AuthenticationTicket>>().To<TicketSerializer>();
            _kernel.Bind<IDataProtector>().ToMethod(x => Startup.DataProtectionProvider.Create(appName));
            _kernel.Bind<ISecureDataFormat<AuthenticationTicket>>().To<SecureDataFormat<AuthenticationTicket>>();
            _kernel.Bind<ITextEncoder>().To<Base64UrlTextEncoder>();
            _kernel.Bind<IAuthenticationManager>()
                .ToMethod(c => HttpContext.Current.GetOwinContext().Authentication)
                .InRequestScope();
            _kernel.Bind<IdentityFactoryOptions<ApplicationUserManager>>()
                .ToMethod(x => new IdentityFactoryOptions<ApplicationUserManager>()
                {
                    DataProtectionProvider = Startup.DataProtectionProvider
                });

        }

        public void Dispose()
        {
            _kernel?.Dispose();
        }
    }
}