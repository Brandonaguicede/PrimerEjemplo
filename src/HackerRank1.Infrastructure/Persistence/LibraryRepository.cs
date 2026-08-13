using HackerRank1.Application.Abstractions;
using HackerRank1.Domain.Models;
using HackerRank1.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HackerRank1.Infrastructure.Persistence;

public class LibraryRepository : ILibraryRepository
{
    private readonly LibraryContext _context;

    public LibraryRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Library>> GetAllAsync()
    {
        return await _context.Libraries.ToListAsync();
    }

    public Task<Library?> GetByIdAsync(int id)
    {
        return _context.Libraries.SingleOrDefaultAsync(l => l.Id == id);
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _context.Libraries.AnyAsync(l => l.Id == id);
    }

    public async Task<Library> AddAsync(Library library)
    {
        await _context.Libraries.AddAsync(library);
        await _context.SaveChangesAsync();
        return library;
    }

    public async Task<Library> UpdateAsync(Library library)
    {
        _context.Libraries.Update(library);
        await _context.SaveChangesAsync();
        return library;
    }

    public async Task DeleteAsync(Library library)
    {
        _context.Libraries.Remove(library);
        await _context.SaveChangesAsync();
    }
}
