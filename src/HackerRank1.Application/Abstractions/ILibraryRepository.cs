using HackerRank1.Domain.Models;

namespace HackerRank1.Application.Abstractions;

public interface ILibraryRepository
{
    Task<IReadOnlyList<Library>> GetAllAsync();

    Task<Library?> GetByIdAsync(int id);

    Task<bool> ExistsAsync(int id);

    Task<Library> AddAsync(Library library);

    Task<Library> UpdateAsync(Library library);

    Task DeleteAsync(Library library);
}
