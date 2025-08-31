namespace JaMoveoApp.Dtos
{
    public class SignupDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Instrument { get; set; } = null!;
        public bool IsAdmin { get; set; } = false!;

    }
}
