



namespace JaMoveoApp.Hubs
{
    public class SongHub : Hub
    {

        //public async Task NotifySongSelected(SongDto song)
        //{
        //    _lastSelectedSong = song;
        //    await Clients.All.SendAsync("SongSelected", song); // שולח לכל המשתמשים
        //}

        public override async Task OnConnectedAsync()
        {

            if (SongsController.LastSelectedSong != null)
            {
                await Clients.All.SendAsync("SongSelected", SongsController.LastSelectedSong);
            }
            await base.OnConnectedAsync();
        }



        public override async Task OnDisconnectedAsync(Exception exception)
        {

            await base.OnDisconnectedAsync(exception);
        }
    }
}
