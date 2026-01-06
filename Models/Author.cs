namespace LibraryApi.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string First_Name { get; set; } = null!;
        public string Last_Name { get; set; } = null!;

        public List<Book> Books { get; set; } = new();
    }
}
