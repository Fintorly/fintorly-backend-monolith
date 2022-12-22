using System;
namespace Fintorly.Infrastructure.Utilities
{
    public static class Messages
    {
        public static class General
        {
            public static string ValidationError()
            {
                return "Bir Veya Daha Fazla Validasyon Hatası İle Karşılaşıldı.";
            }
            public static string NullFieldError(string field)
            {
                return $"{field} alanı boş";
            }
            public static string NotFoundArgument(string field)
            {
                return $"Böyle bir {field} bulunamadı.";
            }
            public static string NotEqualValue()
            {
                return "Yanlış değer.";
            }
            public static string ExistArgument(string field)
            {
                return $"Bu {field} mevcut.";
            }
        }
    }
}

