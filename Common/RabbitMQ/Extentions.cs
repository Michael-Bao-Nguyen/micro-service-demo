using Common.Setting;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.RabbitMQ
{
    public static class Extentions
    {
        public static IServiceCollection AddMassTransitService(this IServiceCollection services )
        {
            services.AddMassTransit(configure => {
                configure.AddConsumers(Assembly.GetEntryAssembly());
                configure.UsingRabbitMq((context, configurator) =>
                {
                    var configuration = context.GetService<IConfiguration>();
                    var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSetting)).Get<RabbitMQSetting>();
                    configurator.Host(rabbitMQSettings.Host, "/", h =>
                    {
                        h.Username("user");
                        h.Password("password");
                    });
                    configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("Catalog", false));
                });         
            });
            services.AddMassTransitHostedService();
            return services;
        }
    }
}
