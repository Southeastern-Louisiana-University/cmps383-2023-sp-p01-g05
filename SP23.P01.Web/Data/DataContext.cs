using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Entities;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<TrainStation> TrainStations { get; set; }//this might cause an issue.If remove this update the controller..

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);//or this causing the issues
       
    }
}
