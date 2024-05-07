namespace WebAPI.Viewmodels.Books
{
    public class Bookrespon
    {
        public int BookId { get; set; }

        public string? BookName { get; set; }

        public int BookTypeId { get; set; }

        public string? BookIsbn { get; set; }

        public int? BookCost { get; set; }

        public int? BookPrice { get; set; }

        public bool? BookStatus { get; set; }

        public string? ImageFile { get; set; }
        
        public string? BooktypeName { get; set; } 

        public int? NumStock { get; set;}
    }
}
