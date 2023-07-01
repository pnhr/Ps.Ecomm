using MassTransit;
using Ps.Ecomm.DataAccess;
using Ps.Ecomm.DataAccess.Definition;
using Ps.Ecomm.Listeners;
using Ps.Ecomm.Models;

namespace Ps.Ecomm.AppStart
{
    public static class AppServices
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Ps Ecomm Service",
                    Version = "v1"
                });
            });

            services.AddHttpClient("order", config => config.BaseAddress = new System.Uri("https://localhost:7291"));

            var connectionString = config.GetConnectionString("AppDb");
            var rabbitMqConnStr = config.GetConnectionString("RabbitMqConnStr");

            services.AddSingleton<IProductProvider>(new ProductProvider(connectionString));
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(rabbitMqConnStr);
                });
            });
            //services.AddSingleton<IConnectionProvider>(new ConnectionProvider(rabbitMqConnStr));
            //services.AddScoped<IPublisher>(x => new Publisher(x.GetService<IConnectionProvider>(),
            //                                                    MQConstants.EXCHANGE_REPORT,
            //                                                    ExchangeType.Topic));

            services.AddHostedService<OrderCreatedListener>();
        }
    }
}
