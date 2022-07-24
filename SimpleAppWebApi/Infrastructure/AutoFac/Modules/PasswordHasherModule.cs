using Autofac;
using Microsoft.AspNetCore.Identity;
using SimpleApp.Core.Models;

namespace SimpleApp.WebApi.Infrastructure.AutoFac.Modules
{
    public class PasswordHasherModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>();
        }
    }
}
