using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Entities;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<TrainStation> TrainStations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

    }
}

//modelBuilder.ApplyConfiguration(new TrainStationConfiguration());