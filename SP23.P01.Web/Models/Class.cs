using Microsoft.EntityFrameworkCore;

namespace SP23.P01.Web.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dataContext = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                if(dataContext.TrainStations.Any()) 
                {
                    return;
                }
                dataContext.TrainStations.AddRange(
                    new TrainStation
                    {
                        Id = 1,
                        Name = "Hammond",
                        Address = "404 NW Railroad Ave",
                    });
                //add more seeded data
                dataContext.SaveChanges();
            }

        }
    }
}
