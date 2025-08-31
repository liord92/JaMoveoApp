namespace JaMoveoApp.Dtos
{
    public record LoginResultDto(bool Success, string Message, UserDto? User);
}
