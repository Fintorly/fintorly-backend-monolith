using Fintorly.Application.Dtos.OneSignalNotificationDtos;
using Fintorly.Application.Interfaces.Utils;
using OneSignalApi.Api;
using OneSignalApi.Client;
using OneSignalApi.Model;

namespace Fintorly.Application.Utilities
{
    public class OneSignalService : IOneSignalServiceService
    {
        string apiKey = "Yzc3NDFhNzYtNTgyZS00NWZkLWIyYTEtNTZlYjgzOGU5MGFj";
        //string apiKey = "NTY5ZjY3ZDUtZTQ5ZC00ODIxLWIwYTgtMjM0NDA3NDQ0YmIx";

        string appid = "c315b5d7-b384-41db-a217-81127e403a13";
        //string appid = "e771be2f-d2a7-4134-9782-15261f0bd536";

        Configuration appConfig = new Configuration();


        DefaultApi appInstance;
        IMapper mapper;
        public OneSignalService(IMapper mapper)
        {
            //appConfig.BasePath = "https://onesignal.com/api/v1";
            //appConfig.AccessToken = apiKey;
            //appInstance = new DefaultApi(appConfig);

            var appConfig = new Configuration();
            appConfig.BasePath = "https://onesignal.com/api/v1";
            appConfig.AccessToken = apiKey;
            appInstance = new DefaultApi(appConfig);
            this.mapper = mapper;
        }

        public async Task<IResult> CreateNewNotification(OneSignalNotificationDto oneSignalNotificationDto)
        {
            List<ButtonDto> buttonDtos = new List<ButtonDto>();

            if (oneSignalNotificationDto.Button1 is not null)
                buttonDtos.Add(oneSignalNotificationDto.Button1);
            if (oneSignalNotificationDto.Button2 is not null)
                buttonDtos.Add(oneSignalNotificationDto.Button2);
            if (oneSignalNotificationDto.Button3 is not null)
                buttonDtos.Add(oneSignalNotificationDto.Button3);

            List<Button> buttons = mapper.Map<List<Button>>(buttonDtos);
            string imagePath = "";
            if (oneSignalNotificationDto.File is not null)
            {
                var result = FileUpload.UploadAlternative(oneSignalNotificationDto.File, "Notification");
                imagePath = result.Data.ToString();
            }

            var notification = new Notification(appId: appid)
            {
                Headings = new StringMap(en: oneSignalNotificationDto.Headings),
                Subtitle = new StringMap(en: oneSignalNotificationDto.Subtitle),
                Contents = new StringMap(en: oneSignalNotificationDto.Contents),
                IncludedSegments = new List<string> { "Subscribed Users" },
                Url = oneSignalNotificationDto.Url,
                BigPicture = imagePath != "" ? imagePath : oneSignalNotificationDto.BigPictureUrl,
                Buttons = buttons,
                SendAfter = oneSignalNotificationDto.SendAfter != null ? oneSignalNotificationDto.SendAfter : DateTime.Now
            };

            var response = await appInstance.CreateNotificationAsync(notification);

            return Result.Success("İşlem Başarı ile gerçekleşti", notification);
        }



    }
}

