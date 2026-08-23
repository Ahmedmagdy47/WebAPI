namespace WebAPI.Services
{
    public interface INotificationService
    {
        Task SendNewPollNotificationAsync(int? pollId = null);
    }
}
