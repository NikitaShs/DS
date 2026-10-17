using Microsoft.AspNetCore.Identity;

namespace AuthServise.Domain
{
    public sealed class Account : IdentityUser<Guid>
    {

        private Account()
        {

        }

        public Account(string email, string userName)
        {
            Id = Guid.NewGuid();
            Email = email;
            UserName = userName;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }


        public string? DisplayName { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set;}


        public void SetDisplayName(string? displayName)
        {
            DisplayName = displayName;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
