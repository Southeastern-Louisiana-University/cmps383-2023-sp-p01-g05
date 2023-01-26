namespace SP23.P01.Web.Entities
{
    public class TrainStation
    {
        // public DateOnly Date { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }

    public class TrainStationDto
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

    }

}