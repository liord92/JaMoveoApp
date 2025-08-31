namespace JaMoveoApp.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void MapAllHubs(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<SongHub>("/songHub");
        }
    }
}
