using Autofac;
using SimpleApp.Core;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Logics;
using System.Linq;

namespace SimpleApp.WebApi.Infrastructure.AutoFac.Modules
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterAssemblyTypes(typeof(Result).Assembly)
                .Where(t => typeof(ILogic).IsAssignableFrom(t))
                .AsImplementedInterfaces();
        }
    }
}
