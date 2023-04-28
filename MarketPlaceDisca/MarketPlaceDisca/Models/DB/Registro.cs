namespace MarketPlaceDisca.Models.DB
{
    public class Registro
    {
        public string IdUser { get; set; } = null!;

        public string NameUser { get; set; } = null!;

        public string LastNameUser { get; set; } = null!;

        public DateOnly BirthDate { get; set; } 
        public string Email { get; set; } = null!;

        public string TypeDocument { get; set; } = null!;

        public string Gender { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
