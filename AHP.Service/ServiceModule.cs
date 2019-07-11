using AHP.Service.Common;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service
{
    public class ServiceModule : Autofac.Module
    {   //Register components in Load()
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Consistency>().As<IConsistency>();
            builder.RegisterType<MatrixFiller>().As<IMatrixFiller>();
            builder.RegisterType<FinalScoreCalculator>().As<IFinalScoreCalculator>();
            builder.RegisterType<VectorFiller>().As<IVectorFiller>();

            builder.RegisterType<CalculateAHPScores>().As<ICalculateAHPScores>();

            //builder.RegisterType<UserLoginService>().As<IUserLoginService>();
            //builder.RegisterType<UserRegisterService>().As<IUserRegisterService>();
            //builder.RegisterType<UserDeleteService>().As<IUserDeleteService>();
            //builder.RegisterType<UserUpdateService>().As<IUserUpdateService>();

            //Registers all classes ending with Service
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces();
            

        }
    }
}
