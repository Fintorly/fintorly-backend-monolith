using System;
namespace Fintorly.Domain.Utils
{
    public static class VerificationCodeGenerator
    {
        public static string Generate()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}

