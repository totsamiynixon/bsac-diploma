using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Infrastructure
{
    public interface IDependencyBuilder
    {
        void ApplyBindings(IKernel _kernel);
    }
}
