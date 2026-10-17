using AuthServise.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.Postgres.Configurations.Identity;

internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("accounts");

        builder.Property(a => a.Id).HasColumnName("id");

        builder.Property(a => a.UserName)
            .HasColumnName("username")
            .HasMaxLength(AccountConstants.USERNAME_MAX_LENGTH);

        builder.Property(a => a.NormalizedUserName)
            .HasColumnName("normalized_username")
            .HasMaxLength(AccountConstants.USERNAME_MAX_LENGTH);

        builder.Property(a => a.Email)
            .HasColumnName("email")
            .HasMaxLength(AccountConstants.EMAIL_MAX_LENGTH);

        builder.Property(a => a.NormalizedEmail)
            .HasColumnName("normalized_email")
            .HasMaxLength(AccountConstants.EMAIL_MAX_LENGTH);

        builder.Property(a => a.EmailConfirmed).HasColumnName("email_confirmed");
        builder.Property(a => a.PasswordHash).HasColumnName("password_hash");
        builder.Property(a => a.SecurityStamp).HasColumnName("security_stamp");
        builder.Property(a => a.ConcurrencyStamp).HasColumnName("concurrency_stamp");
        builder.Property(a => a.PhoneNumber).HasColumnName("phone_number");
        builder.Property(a => a.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed");
        builder.Property(a => a.TwoFactorEnabled).HasColumnName("two_factor_enabled");
        builder.Property(a => a.LockoutEnd).HasColumnName("lockout_end");
        builder.Property(a => a.LockoutEnabled).HasColumnName("lockout_enabled");
        builder.Property(a => a.AccessFailedCount).HasColumnName("access_failed_count");

        builder.Property(a => a.DisplayName)
            .HasColumnName("display_name")
            .HasMaxLength(AccountConstants.DISPLAY_NAME_MAX_LENGTH);

        builder.Property(a => a.CreatedAt).HasColumnName("created_at");
        builder.Property(a => a.UpdatedAt).HasColumnName("updated_at");

    }
}