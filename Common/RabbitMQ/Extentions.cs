using Common.Setting;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.RabbitMQ
{
    public static class Extentions
    {
        public static IServiceCollection AddMss(this IServiceCollection services)
        {
            services.AddMassTransit(configure => {
                configure.AddConsumers(Assembly.GetEntryAssembly());
                configure.UsingRabbitMq((context, configurator) =>
                {
                    var configuration = context.GetService<IConfiguration>();
                    var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSetting)).Get(RabbitMQSetting);
                    configurator.Host(rabbitMQSettings.Host, "/", h =>
                    {
                        h.Username("user");
                        h.Password("password");
                    });
                    configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("Catalog", false));
                });         
            });
            return services.AddMassTransitHostedService();

        }
    }
}
