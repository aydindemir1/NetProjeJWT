using Microsoft.AspNetCore.Identity;

namespace NetProjeJWT.Identities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;

        public DateTime? BirthDate { get; set; }
    }
}
