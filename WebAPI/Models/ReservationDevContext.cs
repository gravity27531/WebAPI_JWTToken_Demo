using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models;

public partial class ReservationDevContext : DbContext
{
    public ReservationDevContext()
    {
    }

    public ReservationDevContext(DbContextOptions<ReservationDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookStock> BookStocks { get; set; }

    public virtual DbSet<BookType> BookTypes { get; set; }

    public virtual DbSet<Flagnoti> Flagnotis { get; set; }

    public virtual DbSet<GenerateNumber> GenerateNumbers { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleMenu> RoleMenus { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleItem> SaleItems { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Thai_100_CI_AS");

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Book");

            entity.Property(e => e.BookIsbn)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BookName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImageFile).IsUnicode(false);
            entity.Property(e => e.InsertBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InsertDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.BookType).WithMany(p => p.Books)
                .HasForeignKey(d => d.BookTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_BookType");
        });

        modelBuilder.Entity<BookStock>(entity =>
        {
            entity.HasKey(e => e.BookStockId).HasName("PK_BookStock_1");

            entity.ToTable("BookStock");

            entity.Property(e => e.InsertBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InsertDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Book).WithMany(p => p.BookStocks)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookStock_Book");
        });

        modelBuilder.Entity<BookType>(entity =>
        {
            entity.ToTable("BookType");

            entity.Property(e => e.BooktypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InsertBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InsertDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Flagnoti>(entity =>
        {
            entity.HasKey(e => e.FlagnotiId).HasName("PK_Flagniti");

            entity.ToTable("Flagnoti");

            entity.Property(e => e.Flagnotiname)
                .HasMaxLength(3)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GenerateNumber>(entity =>
        {
            entity.HasKey(e => new { e.Year, e.Month }).HasName("PK_GennerateID");

            entity.ToTable("GenerateNumber");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.Property(e => e.Action).HasMaxLength(1000);
            entity.Property(e => e.Area).HasMaxLength(1000);
            entity.Property(e => e.Controller).HasMaxLength(1000);
            entity.Property(e => e.Detail).HasMaxLength(1000);
            entity.Property(e => e.InsertBy).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(10);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK_Person_1");

            entity.ToTable("Person");

            entity.Property(e => e.InsertBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InsertDate).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PersonAddress)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PersonCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PersonName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PersonTel)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Role).WithMany(p => p.Persons)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Role_Person");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.InsertBy).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(10);
        });

        modelBuilder.Entity<RoleMenu>(entity =>
        {
            entity.HasKey(e => new { e.MenuId, e.RoleId }).HasName("PK_RoleMenus_1");

            entity.Property(e => e.InsertBy).HasMaxLength(10);

            entity.HasOne(d => d.Menu).WithMany(p => p.RoleMenus)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleMenus_Menus1");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleMenus)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleMenus_Role1");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK_01");

            entity.ToTable("Sale");

            entity.Property(e => e.InsertBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InsertDate).HasColumnType("datetime");
            entity.Property(e => e.SaleCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Person).WithMany(p => p.Sales)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sale_Person");

            entity.HasOne(d => d.Status).WithMany(p => p.Sales)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sale_Status");
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.ToTable("SaleItem");

            entity.Property(e => e.InsertBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InsertDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Book).WithMany(p => p.SaleItems)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SaleItem_Book");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleItems)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SaleItem_Sale");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.StatusId).ValueGeneratedNever();
            entity.Property(e => e.InsertBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InsertDate).HasColumnType("datetime");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.TokenId);

            entity.ToTable("Token");

            entity.Property(e => e.TokenId).ValueGeneratedOnAdd(); // Use auto-generated values
            entity.Property(e => e.PersonId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Tokenkey)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Exp).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
