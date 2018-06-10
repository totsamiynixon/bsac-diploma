using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Web.Common;
using Services;
using Services.Interfaces;

namespace WebUI.Infrastructure
{
    public class AdminDependencyBinder : IDependencyBuilder
    {
        public void ApplyBindings(IKernel _kernel)
        {
            _kernel.Bind<ICriteriaService>().To<CriteriaService>().InRequestScope();
            _kernel.Bind<IExerciseService>().To<ExerciseService>().InRequestScope();
            _kernel.Bind<IProfessionService>().To<ProfessionService>().InRequestScope();
        }
    }
}