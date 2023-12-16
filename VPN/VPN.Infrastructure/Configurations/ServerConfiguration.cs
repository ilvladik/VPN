using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VPN.Domain.Entities;

namespace VPN.Infrastructure.Configurations
{
    public class ServerConfiguration : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.NetworkId)
                .IsRequired();
            builder.Property(s => s.ApiPort)
                .IsRequired();
            builder.Property(s => s.ApiPrefix)
                .IsRequired();
            builder.HasMany(s => s.Keys)
                .WithOne(k => k.Server)
                .HasForeignKey(k => k.ServerId);
        }
    }
}
