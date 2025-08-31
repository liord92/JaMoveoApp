namespace JaMoveoApp.Interfaces
{
    public interface ISongsService
    {
        Task<List<string>> GetAllSongNamesAsync();
        Task<string?> LoadSongFileAsync(string songName);
    }
}
