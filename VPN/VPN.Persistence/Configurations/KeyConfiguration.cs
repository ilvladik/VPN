using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPN.Domain.Entities;

namespace VPN.Persistence.Configurations
{
    internal class KeyConfiguration : IEntityTypeConfiguration<Key>
    {
        public void Configure(EntityTypeBuilder<Key> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.OutlineId)
                .IsRequired();
            builder.Property(k => k.Name)
                .IsRequired();
            builder.Property(k => k.Password)
                .IsRequired();
            builder.Property(k => k.KeyPort)
                .IsRequired();
            builder.Property(k => k.Method)
                .IsRequired();
            builder.HasOne(k => k.Server)
                .WithMany(s => s.Keys)
                .HasForeignKey(k => k.ServerId);
        }
    }
}
