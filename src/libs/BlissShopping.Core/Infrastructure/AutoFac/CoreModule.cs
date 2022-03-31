using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
//using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Module = Autofac.Module;

namespace BlissShopping.Core.Infrastructure.AutoFac
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // MediatR
            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            // Request & Notification Handlers Factory for MediatR
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t =>
                {
                    return c.Resolve(t);
                };
            });

            var executingAssembly = typeof(CoreModule).Assembly;

            // RequestHandlers
            builder.RegisterAssemblyTypes(executingAssembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerDependency();

            // NotificationHandlers
            builder.RegisterAssemblyTypes(executingAssembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>))
                .InstancePerDependency();

            //// Validators
            //builder.RegisterAssemblyTypes(executingAssembly)
            //    .AsClosedTypesOf(typeof(IValidator<>))
            //    .InstancePerDependency();

            // PreProcessors
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerDependency();

            // PostProcessors
            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerDependency();

            //builder.RegisterModule<AutomapperModule>();
        }
    }
}

