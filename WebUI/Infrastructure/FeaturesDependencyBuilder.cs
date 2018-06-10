﻿using Ninject;
using Ninject.Web.Common;
using Services.Features.Implementations;
using Services.Features.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Infrastructure
{
    public class FeaturesDependencyBuilder : IDependencyBuilder
    {
        public void ApplyBindings(IKernel _kernel)
        {
           //FEATURES
            _kernel.Bind<IExerciseService>().To<ExerciseService>().InRequestScope();
        }
    }
}