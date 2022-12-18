namespace Fintorly.Application.Interfaces.Utils
{
    public interface IRedisService
    {
        Task SetStringAsync<T>(string key, T value, DateTime expire);
        Task SetStringAsync<T>(string key, T value);
        Task<T> GetStringAsync<T>(string key);
    }
}

