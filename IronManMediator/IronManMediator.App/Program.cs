using IronManMediator.App.HostedServices;
using IronManMediator.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IronManMediator.App;

public class Program
{
    public static void Main(string[] args)
    {
        Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddIronManMediator(config =>
                {
                    config.RegisterFromAssemblyContaining<Program>();
                });
                services.AddHostedService<TestHostedService>();
            })
            .Build()
            .Run();
    }
}