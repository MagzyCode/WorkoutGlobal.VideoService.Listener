using MassTransit;
using Refit;
using System.Text.Json;
using WorkoutGlobal.Shared.Messages;
using WorkoutGlobal.VideoService.Listener.Contracts;

namespace WorkoutGlobal.VideoService.Listener.Consumers
{
    public class UpdateUserConsumer : IConsumer<UpdateUserMessage>
    {
        public UpdateUserConsumer(IConfiguration configuration)
        {
            Configuration = configuration;
            VideoEndpoint = RestService.For<IVideoController>(Configuration["MassTransitSettings:ConsumerUrl"]);
        }

        public IConfiguration Configuration { get; }

        public IVideoController VideoEndpoint { get; }

        public async Task Consume(ConsumeContext<UpdateUserMessage> context)
        {
            var message = context.Message;

            var document = new
            {
                op = "replace",
                path = "/CreatorFullName",
                value = $"{message?.FirstName} {message?.LastName} {message?.Patronymic}"
            };

            var patchDocument = $"[{JsonSerializer.Serialize(document)}]";

            await VideoEndpoint.UpdateCreator(message.UpdationId, patchDocument);
        }
    }
}
