using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagementApplication.Models
{
    public class UserModel
    {
        [Key]
        [Column("UserId", TypeName ="nvarchar(100)")]
        public string Id { get; set; }

        [Required]
        [Column("UserName", TypeName = "nvarchar(100)")]
        public string UserName { get; set; }

        [Required]
        [Column("Email", TypeName = "nvarchar(100)")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Column("Role", TypeName = "nvarchar(450)")]
        public string Role { get; set; }

        [Required]
        [Column("PasswordHash", TypeName = "nvarchar(450)")]
        public string PasswordHash { get; set; }

        public ICollection<BookModel> Books { get; set; }
    }
}
