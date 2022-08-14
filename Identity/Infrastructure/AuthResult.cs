using myCloudDAL.DAL.Entities.Identity;

namespace Identity.Infrastructure
{
    public class AuthResult
    {
        public bool Sacsseses { get; }
        public string KEY { get; }
        public DateTime Expiration { get; }
        public AppUser User { get; }
        public IList<string> Role { get; }
        public string Status { get; }

        public AuthResult(bool sacsseses, string key, DateTime expiration, AppUser user, IList<string> role, string status)
        {
            Sacsseses = sacsseses;
            KEY = key;
            Expiration = expiration;
            User = user;
            Role = role;
            Status = status;
        }

        public override bool Equals(object? obj)
        {
            return obj is AuthResult other &&
                   Sacsseses == other.Sacsseses &&
                   KEY == other.KEY &&
                   Expiration == other.Expiration &&
                   EqualityComparer<AppUser>.Default.Equals(User, other.User) &&
                   EqualityComparer<IList<string>>.Default.Equals(Role, other.Role) &&
                   Status == other.Status;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Sacsseses, KEY, Expiration, User, Role, Status);
        }
    }
}
