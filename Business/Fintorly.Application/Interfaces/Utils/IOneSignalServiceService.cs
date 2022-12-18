using Fintorly.Application.Dtos.OneSignalNotificationDtos;

namespace Fintorly.Application.Interfaces.Utils
{
    public interface IOneSignalServiceService
    {
        Task<IResult> CreateNewNotification(OneSignalNotificationDto oneSignalNotificationDto);
    }
}

