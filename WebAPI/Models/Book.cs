using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string? BookName { get; set; }

    public int BookTypeId { get; set; }

    public string? BookIsbn { get; set; }

    public int? BookCost { get; set; }

    public int? BookPrice { get; set; }

    public bool? BookStatus { get; set; }

    public string? InsertBy { get; set; }

    public DateTime? InsertDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? ImageFile { get; set; }

    public virtual ICollection<BookStock> BookStocks { get; set; } = new List<BookStock>();

    public virtual BookType BookType { get; set; } = null!;

    public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
}
