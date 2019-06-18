using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service
{
    public class ServiceModule : Module
    {   //Register components in Load()
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserLogin>().As<IUserLogin>();
        }
    }
}
