using Autofac;
using JetBrains.Annotations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.NET._17._Mediator
{
    public class PingCommand : IRequest<PongResponse>
    {

    }

    public class PongResponse
    {
        public DateTime Timestamp { get; set; }

        public PongResponse(DateTime timestamp)
        {
            Timestamp = timestamp;
        }
    }

    [UsedImplicitly]
    public class PingCommandHandler : IRequestHandler<PingCommand, PongResponse>
    {
        public async Task<PongResponse> Handle(PingCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new PongResponse(DateTime.UtcNow))
                .ConfigureAwait(false); // Last step is to avoid any deadlock potencial case
        }
    }

    internal class _3_MediatoR
    {

        public static async Task Drive()
        {
            var builder = new ContainerBuilder();
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder
                .Register<ServiceFactory>(
                    ctx =>
                    {
                        var c = ctx.Resolve<IComponentContext>();
                        return t => c.Resolve(t);
                    }
            );

            builder
                .RegisterAssemblyTypes(typeof(_3_MediatoR).Assembly)
                 .AsImplementedInterfaces();


            var container = builder.Build();
            var mediator = container.Resolve<IMediator>();
            var response = await mediator.Send(new PingCommand());
            Console.WriteLine($"We got a response at {response.Timestamp}");
        }

    }
}
