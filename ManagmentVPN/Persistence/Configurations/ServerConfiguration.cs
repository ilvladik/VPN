

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ServerConfiguration : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.NetworkId)
                .IsRequired();
            builder.Property(s => s.State)
                .IsRequired();
            builder.HasMany(s => s.Keys)
                .WithOne(k => k.Server)
                .HasForeignKey(k => k.ServerId);
        }
    }
}
