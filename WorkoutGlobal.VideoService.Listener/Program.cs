using MassTransit;
using WorkoutGlobal.VideoService.Listener.Consumers;
using WorkoutGlobal.VideoService.Listener.Contracts;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<UpdateUserConsumer>();
    options.AddConsumer<DeleteUserConsumer>();

    options.UsingRabbitMq((cxt, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMqHost"]);

        cfg.ReceiveEndpoint(builder.Configuration["Exchanges:UpdateUser"], endpoint =>
        {
            endpoint.ConfigureConsumer<UpdateUserConsumer>(cxt);
        });

        cfg.ReceiveEndpoint(builder.Configuration["Exchanges:DeleteUser"], endpoint =>
        {
            endpoint.ConfigureConsumer<DeleteUserConsumer>(cxt);
        });
    });
});
builder.Services.AddMassTransitHostedService();

builder.Services.AddRefitClient<IVideoController>()
    .ConfigureHttpClient(configure =>
    {
        configure.BaseAddress = new(builder.Configuration["ConsumerUrl"]);
    });

var app = builder.Build();

app.Run();