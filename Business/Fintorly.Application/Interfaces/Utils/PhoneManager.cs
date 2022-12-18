using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Fintorly.Application.Interfaces.Utils
{
    public class PhoneManager : IPhoneService
    {
        IConfiguration Configuration { get; }
        PhoneSettings _phoneSettings;

        public PhoneManager(IConfiguration configuration)
        {
            Configuration = configuration;
            _phoneSettings = Configuration.GetSection("PhoneSettings").Get<PhoneSettings>();
        }

        public async Task<IResult> SendPhoneVerificationCodeAsync(string phoneNumber, string verificationCode)
        {
            string msg = $"Fahax Doğrulama kodunuz: {verificationCode}";
            string tur = "Normal";
            string sms1N = "data=<sms><kno>" + _phoneSettings.Id + "</kno>" +
                           "<kulad>" + _phoneSettings.UserName + "</kulad>" +
                           "<sifre>" + _phoneSettings.Password + "</sifre>" +
                           "<gonderen>" + _phoneSettings.Orginator + "</gonderen>" +
                           "<mesaj>" + msg + "</mesaj>" +
                           "<numaralar>" + phoneNumber + "</numaralar>" +
                           "<tur>" + tur + "</tur></sms>";
            string a = XmlPost("http://panel.vatansms.com/panel/smsgonder1Npost.php", sms1N);
            return Result.Success("Doğrulama kodu başarıyla gönderildi");
        }

        private string XmlPost(string PostAddress, string xmlData)
        {
            using (WebClient wUpload = new WebClient())
            {
                wUpload.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                Byte[] bPostArray = Encoding.UTF8.GetBytes(xmlData);
                Byte[] bResponse = wUpload.UploadData(PostAddress, "POST", bPostArray);
                Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
                string sWebPage = new string(sReturnChars);
                return sWebPage;
            }
        }

        public Task<IResult> SendPhoneContentAsync(string phoneNumber, string verificationCode)
        {
            throw new NotImplementedException();
        }
    }
}

