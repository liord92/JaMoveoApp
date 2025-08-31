namespace JaMoveoApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required string Salt { get; set; }
        public required string Instrument { get; set; }
        public required bool IsAdmin { get; set; }
    }
}
