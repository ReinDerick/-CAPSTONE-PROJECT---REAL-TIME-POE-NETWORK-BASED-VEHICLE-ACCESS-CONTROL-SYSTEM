namespace BsuAdmin.Models
{
    public class AddPlateNumberDto
    {
        public required string PlateNumber { get; set; }

        public required string Name { get; set; }

        public required string ContactNumber { get; set; }

        public DateTime Date { get; set; }
    }
}
