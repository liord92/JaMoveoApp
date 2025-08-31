
namespace JaMoveoApp.Services
{
    public class SongsService : ISongsService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _songsFolderPath;
        private readonly IWebHostEnvironment _env;
        public SongsService(IWebHostEnvironment env, ApplicationDbContext context)
        {
            // קובע את הנתיב הפיזי לתיקייה שמכילה את קבצי השירים
            _songsFolderPath = Path.Combine(env.ContentRootPath, "SongsFiles");
            _env = env;
            _context = context;

        }

        public async Task<List<string>> GetAllSongNamesAsync()
        {
            if (!Directory.Exists(_songsFolderPath))
                return new List<string>();

            return await Task.Run(() =>
            {
                return Directory.GetFiles(_songsFolderPath, "*.json")
                                .Select(file => Path.GetFileNameWithoutExtension(file))
                                .ToList();
            });
        }
        public async Task<string?> LoadSongFileAsync(string songName)
        {
            var safeName = Path.GetFileNameWithoutExtension(songName);
            var path = Path.Combine(_songsFolderPath, $"{safeName}.json");

            if (!File.Exists(path))
                return null;

            return await File.ReadAllTextAsync(path);
        }
        
    }
}
