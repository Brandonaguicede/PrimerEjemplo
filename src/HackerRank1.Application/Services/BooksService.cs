using HackerRank1.Application.Abstractions;
using HackerRank1.Application.Common;
using HackerRank1.Application.DTO;
using HackerRank1.Domain.Models;

namespace HackerRank1.Application.Services;

public class BooksService : IBooksService
{
    private readonly IBookRepository _books;
    private readonly ILibraryRepository _libraries;

    public BooksService(IBookRepository books, ILibraryRepository libraries)
    {
        _books = books;
        _libraries = libraries;
    }

    public async Task<ServiceResult<IEnumerable<BookForm>>> Get(int libraryId, int[]? ids)
    {
        if (!await _libraries.ExistsAsync(libraryId))
            return ServiceResult<IEnumerable<BookForm>>.NotFound($"Library {libraryId} was not found.");

        var books = await _books.GetByLibraryIdAsync(libraryId, ids);
        return ServiceResult<IEnumerable<BookForm>>.Success(books.Select(ToForm).ToList());
    }

    public async Task<ServiceResult<BookForm>> Add(int libraryId, BookForm form)
    {
        if (!await _libraries.ExistsAsync(libraryId))
            return ServiceResult<BookForm>.NotFound($"Library {libraryId} was not found.");

        var book = await _books.AddAsync(new Book
        {
            Id = form.Id,
            Name = form.Name,
            Category = form.Category ?? string.Empty,
            LibraryId = libraryId
        });

        return ServiceResult<BookForm>.Success(ToForm(book));
    }

    public async Task<ServiceResult<BookForm>> Update(int libraryId, int bookId, BookForm form)
    {
        if (!await _libraries.ExistsAsync(libraryId))
            return ServiceResult<BookForm>.NotFound($"Library {libraryId} was not found.");

        var book = await _books.GetByIdAsync(libraryId, bookId);
        if (book == null)
            return ServiceResult<BookForm>.NotFound($"Book {bookId} was not found.");

        book.Name = form.Name;
        book.Category = form.Category ?? string.Empty;

        var updated = await _books.UpdateAsync(book);
        return ServiceResult<BookForm>.Success(ToForm(updated));
    }

    public async Task<ServiceResult> Delete(int libraryId, int bookId)
    {
        if (!await _libraries.ExistsAsync(libraryId))
            return ServiceResult.NotFound($"Library {libraryId} was not found.");

        var book = await _books.GetByIdAsync(libraryId, bookId);
        if (book == null)
            return ServiceResult.NotFound($"Book {bookId} was not found.");

        await _books.DeleteAsync(book);
        return ServiceResult.Success();
    }

    private static BookForm ToForm(Book book)
    {
        return new BookForm
        {
            Id = book.Id,
            Name = book.Name,
            Category = book.Category,
            LibraryId = book.LibraryId
        };
    }
}

public interface IBooksService
{
    Task<ServiceResult<IEnumerable<BookForm>>> Get(int libraryId, int[]? ids);

    Task<ServiceResult<BookForm>> Add(int libraryId, BookForm form);

    Task<ServiceResult<BookForm>> Update(int libraryId, int bookId, BookForm form);

    Task<ServiceResult> Delete(int libraryId, int bookId);
}
