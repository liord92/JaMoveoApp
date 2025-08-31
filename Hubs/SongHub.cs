

namespace JaMoveoApp.Hubs
{
    public class SongHub : Hub
    {
        private static SongDto _lastSelectedSong;
 
        public async Task NotifySongSelected(SongDto song)
        {
            _lastSelectedSong = song;
            await Clients.All.SendAsync("SongSelected", song); // שולח לכל המשתמשים
        }

        public override async Task OnConnectedAsync()
        {
            if (_lastSelectedSong != null)
            {
                await Clients.Caller.SendAsync("SongSelected", _lastSelectedSong);
            }
            await base.OnConnectedAsync();
        }
    }
}
