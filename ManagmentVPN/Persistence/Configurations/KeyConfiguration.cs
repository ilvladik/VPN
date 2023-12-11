

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class KeyConfiguration : IEntityTypeConfiguration<Key>
    {
        public void Configure(EntityTypeBuilder<Key> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Password)
                .IsRequired();
            builder.Property(k => k.KeyPort)
                .IsRequired();
            builder.Property(k => k.Method)
                .IsRequired();
            builder.HasOne(k => k.Server)
                .WithMany(s => s.Keys)
                .HasForeignKey(k => k.ServerId);
            builder.HasOne(k => k.User)
                .WithOne(u => u.Key)
                .HasForeignKey<Key>(k => k.UserId);
        }
    }
}
