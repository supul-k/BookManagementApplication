namespace BookManagementApplication.DTO
{
    public class BookUpdateDTO
    {
        public string? Name { get; set; }

        public string? Author { get; set; }

        public bool? IsBorrowed { get; set; }

        public string? UserId { get; set; }
    }
}
