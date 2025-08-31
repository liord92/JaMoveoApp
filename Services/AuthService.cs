

namespace JaMoveoApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserAsync(string username, string password, string Instrument, bool IsAdmin)
        {
            if (await _context.Users.AnyAsync(u => u.Username == username))
                return false;

            byte[] saltBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);

            string passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                Salt = salt,
                Instrument = Instrument,
                IsAdmin = IsAdmin
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<LoginResultDto> LoginAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return new LoginResultDto(false, "Username does not exist", null);
            }

            byte[] saltBytes = Convert.FromBase64String(user.Salt);

            string inputHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (inputHash != user.PasswordHash)
            {
                return new LoginResultDto(false, "Incorrect password", null);
            }
            return new LoginResultDto(true, "Login successful", new UserDto(user.Username, user.Instrument, user.IsAdmin));
        }
    }
}
