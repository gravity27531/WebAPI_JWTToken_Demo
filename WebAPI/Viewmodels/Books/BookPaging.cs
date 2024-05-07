namespace WebAPI.Viewmodels.Books
{
    public class BookPaging
    {
        public int CurrentPage { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalItems { get; set; }

        public List<Bookrespon> ViewmodelBooks { get; set; }
    }
}
