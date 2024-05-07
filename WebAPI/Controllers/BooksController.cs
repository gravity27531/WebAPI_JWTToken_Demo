using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Viewmodels.Books;
using WebAPI.Viewmodels.Persons;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ReservationDevContext _context;
        public BooksController(ReservationDevContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> GetBookDetailsAsync(BookParam bookParams)
        {
            try
            {
                BookPaging viewModelBookpagings = new BookPaging();

                if (bookParams.Currentpage == null || bookParams.Currentpage == 0)
                {
                    bookParams.Currentpage = 1;
                }

                int maxrowsperpage;
                maxrowsperpage = 10;

                IQueryable<Bookrespon> query = from a in _context.Books
                                               join b in _context.BookTypes on a.BookTypeId equals b.BookTypeId
                                               join c in _context.BookStocks on a.BookId equals c.BookId
                                               
                                               where a.BookStatus == true || a.BookName == bookParams.search
                                               select new Bookrespon
                                               {
                                                   BookId = a.BookId,
                                                   BookName = a.BookName,
                                                   BookTypeId = a.BookTypeId,
                                                   BooktypeName = b.BooktypeName,
                                                   NumStock = (int)c.NumStock,
                                                   BookIsbn = a.BookIsbn,
                                                   BookCost = (int)a.BookCost,
                                                   BookPrice = (int)a.BookPrice,
                                                   ImageFile = a.ImageFile
                                               };
                if (!string.IsNullOrEmpty(bookParams.search))
                {
                    if(bookParams.SearchType == "Name")
                    {
                        query = query.Where(a => a.BookName.Contains(bookParams.search));
                    }
                    else if(bookParams.SearchType == "TypeName")
                    {
                        query = query.Where(a => a.BooktypeName.Contains(bookParams.search));
                    }
                    else if (bookParams.SearchType == "Price")
                    {
                        query = query.Where(a => a.BookPrice == bookParams.searchValueNumeric);
                    }
                }
                viewModelBookpagings.ViewmodelBooks = await query.OrderByDescending(x => x.BookId)
                                                        .Skip((bookParams.Currentpage - 1) * maxrowsperpage)
                                                        .Take(maxrowsperpage)
                                                        .ToListAsync();

                int Scount = await query.CountAsync();

                int totalPages = (int)Math.Ceiling((decimal)Scount / (decimal)maxrowsperpage);
                int startPage = bookParams.Currentpage - 5;
                int endPage = bookParams.Currentpage + 4;

                if (startPage <= 0)
                {
                    endPage = endPage - (startPage - 1);
                    startPage = 1;
                }

                if (endPage > totalPages)
                {
                    endPage = totalPages;
                    if (endPage > 10)
                    {
                        startPage = endPage - 9;
                    }
                }

                viewModelBookpagings.PageCount = Scount;
                viewModelBookpagings.CurrentPage = bookParams.Currentpage;
                viewModelBookpagings.PageSize = maxrowsperpage;
                viewModelBookpagings.TotalPages = totalPages;
                viewModelBookpagings.StartPage = startPage;
                viewModelBookpagings.EndPage = endPage;

                return Ok(viewModelBookpagings);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
