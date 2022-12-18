using System;
using Microsoft.AspNetCore.Http;

namespace Fintorly.Application.Dtos.OneSignalNotificationDtos
{
    public class OneSignalNotificationDto
    {
        public string Headings { get; set; }
        public string Subtitle { get; set; }
        public string Contents { get; set; }
        public string? Url { get; set; }
        public string? BigPictureUrl { get; set; }
        public ButtonDto? Button1 { get; set; }
        public ButtonDto? Button2 { get; set; }
        public ButtonDto? Button3 { get; set; }
        //public List<ButtonDto>? Buttons { get; set; }

        public DateTime SendAfter { get; set; }


        public IFormFile? File { get; set; }


        //public StringMap Headings { get; set; }
        //public StringMap Subtitle { get; set; }
        //public StringMap Contents { get; set; }
        //public List<string> IncludedSegments { get; set; }
        //public string Url { get; set; }
        //public string BigPictureUrl { get; set; }
        //public List<Button> Buttons { get; set; }
        //public DateTime SendAfter { get; set; }
    }
}

