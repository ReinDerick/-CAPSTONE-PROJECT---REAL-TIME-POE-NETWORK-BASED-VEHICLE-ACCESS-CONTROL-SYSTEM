namespace BsuAdmin.Models.Entities
{
    public class AdminRegister
    {

        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
     
    }
}
