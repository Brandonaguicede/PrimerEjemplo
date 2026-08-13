using HackerRank1.Application.Abstractions;
using HackerRank1.Domain.Models;
using HackerRank1.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HackerRank1.Infrastructure.Persistence;

public class BookRepository : IBookRepository
{
    private readonly LibraryContext _context;

    public BookRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Book>> GetByLibraryIdAsync(int libraryId, int[]? ids = null)
    {
        var query = _context.Books.AsQueryable().Where(b => b.LibraryId == libraryId);

        if (ids != null && ids.Any())
            query = query.Where(b => ids.Contains(b.Id));

        return await query.ToListAsync();
    }

    public Task<Book?> GetByIdAsync(int libraryId, int bookId)
    {
        return _context.Books.SingleOrDefaultAsync(b => b.LibraryId == libraryId && b.Id == bookId);
    }

    public async Task<Book> AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task DeleteAsync(Book book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}
