
namespace JaMoveoApp.Interfaces
{
    public interface IAuthService
    {
        Task<bool> CreateUserAsync(string username, string password, string Instrument, bool IsAdmin);
        Task<LoginResultDto> LoginAsync(string username, string password);

    }
}
