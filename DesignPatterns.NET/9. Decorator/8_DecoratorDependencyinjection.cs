using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._9._Decorator
{
    public interface IReportingService
    {
        void Report();
    }

    public class ReportingService : IReportingService
    {
        public void Report()
        {
            Console.WriteLine("Here is your Report");
        }
    }

    public class RepotingServiceWithLogging : IReportingService
    {
        private IReportingService reportingService;

        public RepotingServiceWithLogging(IReportingService reportingService)
        {
            this.reportingService = reportingService;
        }

        public void Report()
        {
            Console.WriteLine("Commenting log ...");
            reportingService.Report();
            Console.WriteLine("Ending log ...");
        }
    }


    internal class _8_DecoratorDependencyinjection
    {
        public static void Drive()
        {
            var c = new ContainerBuilder();
            c.RegisterType<ReportingService>().Named<IReportingService>("reporting");

            c.RegisterDecorator<IReportingService>((context, service) => new RepotingServiceWithLogging(service), "reporting");

            using (var cb = c.Build())
            {
                var r = cb.Resolve<IReportingService>();
                r.Report();
            }

        }
    }
}
