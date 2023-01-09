using System;
namespace Fintorly.Domain.Utils
{
    public static class VerificationCodeGenerator
    {
        public static string Generate()
        {
            var random = new Random();
            return random.Next(10000, 99999).ToString();
        }
    }
}

