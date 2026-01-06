namespace LibraryApi.DTOs
{
    public class BookCreateDto
    {
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public int AuthorId { get; set; }
    }
}
