using WorkoutGlobal.VideoService.Listener.Extensions.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

var busType = Enum.Parse<WorkoutGlobal.VideoService.Listener.Enums.Bus>(builder.Configuration["MassTransitSettings:Bus"]);

builder.Services.ConfigureMassTransit(builder.Configuration, busType);
builder.Services.ConfigureRefitServices(builder.Configuration);

var app = builder.Build();

app.Run();