using Autofac;
using FluentValidation;
using SimpleApp.Core.Interfaces.Logics;

namespace SimpleApp.Web.Infrastructure.AutoFac.Modules
{
    public class ValidationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterAssemblyTypes(typeof(ILogic).Assembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .AsImplementedInterfaces();
        }
    }
}
