namespace MarketPlaceDisca.Models.DB
{
    public class ChangePassword
    {
        public string Email { get; set; }
        public string ContrasenaActual { get; set; }
        public string NuevaContrasena { get; set; }
    }
}
