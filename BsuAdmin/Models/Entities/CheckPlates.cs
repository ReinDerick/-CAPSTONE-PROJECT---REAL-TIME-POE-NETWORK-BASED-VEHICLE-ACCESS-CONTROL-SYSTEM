namespace BsuAdmin.Models.Entities
{
    public class CheckPlates
    {

        public Guid Id { get; set; }

        public required string PlateNumber { get; set; }

        public required string Name { get; set; }

        public required string ContactNumber { get; set; } //string

        public required string Entrance { get; set; }

        public required string Exit { get; set; }

        public bool status { get; set; }

        public string DateLogs { get; set; }
    }
}
