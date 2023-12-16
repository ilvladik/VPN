using ManagmentVPN.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagmentVPN.Infrastructure.Configurations
{
    internal class KeyConfiguration : IEntityTypeConfiguration<Key>
    {
        public void Configure(EntityTypeBuilder<Key> builder)
        {
            builder.HasKey(k => k.Id);
            builder.HasOne(k => k.Server)
                .WithMany(s => s.Keys)
                .HasForeignKey(k => k.ServerId);
            builder.HasOne(k => k.User)
                .WithOne(u => u.Key)
                .HasForeignKey<Key>(k => k.UserId);
        }
    }
}
