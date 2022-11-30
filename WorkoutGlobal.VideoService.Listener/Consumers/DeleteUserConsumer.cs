using MassTransit;
using Refit;
using WorkoutGlobal.Shared.Messages;
using WorkoutGlobal.VideoService.Listener.Contracts;

namespace WorkoutGlobal.VideoService.Listener.Consumers
{
    public class DeleteUserConsumer : IConsumer<DeleteUserMessage>
    {
        public DeleteUserConsumer(IConfiguration configuration)
        {
            Configuration = configuration;
            VideoEndpoint = RestService.For<IVideoController>(Configuration["ConsumerUrl"]);
        }

        public IConfiguration Configuration { get; }

        public IVideoController VideoEndpoint { get; }

        public async Task Consume(ConsumeContext<DeleteUserMessage> context)
        {
            var message = context.Message;

            await VideoEndpoint.DeleteCreatorVideos(message.DeletionId);
        }
    }
}
