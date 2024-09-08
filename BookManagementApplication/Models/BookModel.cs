using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace BookManagementApplication.Models
{
    public class BookModel
    {
        [Key]
        [Column("BookId", TypeName = "nvarchar(100)")]
        public string Id { get; set; }

        [Required]
        [Column("Name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column("Author", TypeName = "nvarchar(100)")]
        public string Author { get; set; }

        [Required]
        [Column("IsBorrowed")]
        public bool IsBorrowed { get; set; }

        [Column("UserId", TypeName = "nvarchar(100)")]
        public string UserId { get; set; }

        public UserModel Users { get; set; }
    }
}
