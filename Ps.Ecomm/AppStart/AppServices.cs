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

            services.AddSingleton<IOrderDetailsProvider, OrderDetailsProvider>();
            services.AddSingleton<IProductProvider>(new ProductProvider(connectionString));
            services.AddSingleton<IInventoryProvider>(new InventoryProvider(connectionString));
            services.AddSingleton<IInventoryUpdator>(new InventoryUpdator(connectionString));

            services.AddMassTransit(config =>
            {
                config.AddConsumer<OrderCreatedConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(rabbitMqConnStr);
                    cfg.ReceiveEndpoint("order-response", c =>
                    {
                        c.ConfigureConsumer<OrderCreatedConsumer>(ctx);
                    });
                });
            });
            services.AddHostedService<OrderCreatedListener>();
        }
    }
}
