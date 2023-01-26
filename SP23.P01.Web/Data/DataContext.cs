using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Entities;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<TrainStation> TrainStations { get; set; }//this might cause an issue

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);//or this causing the issues
       
    }
}

//modelBuilder.ApplyConfiguration(new TrainStationConfiguration());

//modelBuilder.Entity<trainsattion>()
//.Property(t=>t.Name)
//.IsRequired()
//.HasMaxLength(120); ... modelBuilder or use Annotations [Required,MaxLength(120)]
//another way..
//public class TrainStation