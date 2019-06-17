using AHP.Service.Common;
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
            builder.RegisterType<Consistency>().As<IConsistency>();
            builder.RegisterType<MatrixFiller>().As<IMatrixFiller>();
            builder.RegisterType<FinalScoreCalculator>().As<IFinalScoreCalculator>();
            builder.RegisterType<VectorFiller>().As<IVectorFiller>();


        }
    }
}
