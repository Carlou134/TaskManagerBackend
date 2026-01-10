using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain;

namespace TaskManager.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.RoleId);

            builder.HasMany(x => x.Tasks).WithOne(x => x.User).HasForeignKey(x => x.UserId);

            builder.Navigation(x => x.Tasks).UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(x => x.Name).HasMaxLength(200);

            builder.Property(x => x.Email).HasMaxLength(200);

            builder.Property(x => x.PasswordHash).HasMaxLength(200);
        }
    }
}
