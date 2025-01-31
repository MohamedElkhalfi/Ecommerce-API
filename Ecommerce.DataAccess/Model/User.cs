using System.ComponentModel.DataAnnotations; 
 
namespace Ecommerce.DataAccess.Model
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; } // Stocke le hash du mot de passe

        [Required, StringLength(50)]
        public string Role { get; set; } // "Admin", "User"
    }
}
