using Core.Entities;
using MassTransit;
using Worker.Messaging;

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, collection) =>
    {
        collection.AddHttpContextAccessor();

        collection.AddMassTransit(x =>
        {
            x.AddDelayedMessageScheduler();
            x.AddConsumer<OrderConsumer>();
            x.AddRequestClient<OrderEvent>();

            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(context.Configuration.GetConnectionString("RabbitMq"));
                cfg.UseDelayedMessageScheduler();
                cfg.ServiceInstance(instance =>
                {
                    instance.ConfigureJobServiceEndpoints();
                    instance.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));

                });
            });
        });
    }).Build();

await host.RunAsync();
