using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Entities;
namespace SP23.P01.Web.Models
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var dataContext = serviceProvider.GetRequiredService<DataContext>();
            await dataContext.Database.MigrateAsync();

            AddTrainStations(dataContext);
            //add seeded func await AddNext();
        }
        private static void AddTrainStations(DataContext dataContext)
        {

            var stations = dataContext.Set<TrainStation>();

            if (stations.Any())
            {
                return;
            }

            stations.Add(new TrainStation
            {

                Name = "Hammond",
                Address = "404 NW Railroad Ave",
            });

            stations.Add(new TrainStation
            {

                Name = "Covington",
                Address = "505 SE Railroad Ave",
            });
            stations.Add(new TrainStation
            {
                Name = "Gulfport",
                Address = "707 NW Railroad Ave",
            });
            //add more seeded data
            dataContext.SaveChanges();
        }

    }
}

