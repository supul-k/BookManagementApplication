using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookManagementApplication.DTO
{
    public class BookCreateDTO
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public bool IsBorrowed { get; set; }

        public string UserId { get; set; }
    }
}
