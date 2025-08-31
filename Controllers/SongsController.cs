


namespace JaMoveoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ISongsService _songsService;
        private readonly IHubContext<SongHub> _hubContext;
        public SongsController(IWebHostEnvironment env, ISongsService songsService, IHubContext<SongHub> hubContext)
        {
            _env = env;
            _songsService = songsService;
            _hubContext = hubContext;
        }

        [HttpGet("GetSongs")]
        public async Task<IActionResult> GetAllSongs()
        {
            var songs = await _songsService.GetAllSongNamesAsync();
            return Ok(songs);
        }

        [HttpGet("LoadSong/{songName}")]
        public async Task<IActionResult> LoadSong(string songName)
        {
            var content = await _songsService.LoadSongFileAsync(songName);
            if (content == null)
                return NotFound();

            return Content(content, "application/json");
        }

        [HttpPost("select")]
        public async Task<IActionResult> SelectSong([FromBody] SongSelectionDto songName)
        {
            SongDto? song = null;
            var content = await _songsService.LoadSongFileAsync(songName.SongName);
            if (content == null)
                return NotFound();
            var lines = JsonConvert.DeserializeObject<List<List<LineDto>>>(content);
            if (lines != null)
            {
                song = new(songName.SongName, lines);
            }
            
            await _hubContext.Clients.All.SendAsync("SongSelected", song);
            return Ok();
        }

    }
}
