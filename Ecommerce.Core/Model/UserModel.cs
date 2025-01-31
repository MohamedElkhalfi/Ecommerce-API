namespace Ecommerce.Core.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }  // Ajoute cette ligne
        public string Role { get; set; }
    }
}
