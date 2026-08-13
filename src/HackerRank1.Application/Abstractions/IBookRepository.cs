using HackerRank1.Domain.Models;

namespace HackerRank1.Application.Abstractions;

public interface IBookRepository
{
    Task<IReadOnlyList<Book>> GetByLibraryIdAsync(int libraryId, int[]? ids = null);

    Task<Book?> GetByIdAsync(int libraryId, int bookId);

    Task<Book> AddAsync(Book book);

    Task<Book> UpdateAsync(Book book);

    Task DeleteAsync(Book book);
}
