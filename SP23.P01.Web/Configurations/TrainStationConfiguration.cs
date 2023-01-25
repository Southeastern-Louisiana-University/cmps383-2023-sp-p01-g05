using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Entities;

namespace SP23.P01.Web.Configurations
{
    public class TrainStationConfiguration : IEntityTypeConfiguration<TrainStation>
    {
        public void Configure(EntityTypeBuilder<TrainStation> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(120);
            builder.Property(t => t.Address).IsRequired();

        }
    }
}
