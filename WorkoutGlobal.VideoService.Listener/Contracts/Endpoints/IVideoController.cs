using Refit;

namespace WorkoutGlobal.VideoService.Listener.Contracts
{
    [Headers("Content-Type: application/json; charset=utf-8")]
    public interface IVideoController
    {
        [Patch("/api/videos/{updationCreatorId}")]
        public Task UpdateCreator(Guid updationCreatorId, [Body] string document);

        [Delete("/api/videos/creators/{deletionAccountId}")]
        public Task DeleteCreatorVideos(Guid deletionAccountId);
    }
}
