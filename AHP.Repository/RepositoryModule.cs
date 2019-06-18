using AHP.DAL;
using AHP.Repository.Common;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository
{
    class RepositoryModule : Autofac.Module
    {   //Register components in Load()
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AHPEntities>().AsSelf();
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(Repository))).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();

        }
    }
}
