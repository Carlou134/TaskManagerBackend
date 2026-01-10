using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Infrastructure.Persistence.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskManager.Domain.Task>
    {
        public void Configure(EntityTypeBuilder<TaskManager.Domain.Task> builder)
        {
            builder.ToTable("Tasks");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.User).WithMany(x => x.Tasks).HasForeignKey(x => x.UserId);

            builder.Property(x => x.Priority).HasConversion<byte>();
        }
    }
}
