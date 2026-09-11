using System.ComponentModel.DataAnnotations;

namespace BsuAdmin.Models.Entities
{
    public class AddNewPlate
    {
        public Guid Id { get; set; }

        public required string PlateNumber { get; set; } 

        public required string Name { get; set; }

        public required string ContactNumber { get; set; } //string

        public bool isRegistered { get; set; } 

        public DateTime Date { get; set; }
    }
}
