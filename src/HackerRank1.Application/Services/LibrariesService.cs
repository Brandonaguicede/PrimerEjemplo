using HackerRank1.Application.Abstractions;
using HackerRank1.Application.Common;
using HackerRank1.Application.DTO;
using HackerRank1.Domain.Models;

namespace HackerRank1.Application.Services;

public class LibrariesService : ILibrariesService
{
    private readonly ILibraryRepository _libraries;

    public LibrariesService(ILibraryRepository libraries)
    {
        _libraries = libraries;
    }

    public async Task<IEnumerable<LibraryForm>> GetAll()
    {
        var libraries = await _libraries.GetAllAsync();
        return libraries.Select(ToForm);
    }

    public async Task<ServiceResult<LibraryForm>> GetById(int id)
    {
        var library = await _libraries.GetByIdAsync(id);
        return library == null
            ? ServiceResult<LibraryForm>.NotFound($"Library {id} was not found.")
            : ServiceResult<LibraryForm>.Success(ToForm(library));
    }

    public async Task<ServiceResult<LibraryForm>> Add(LibraryForm form)
    {
        var library = await _libraries.AddAsync(ToModel(form));
        return ServiceResult<LibraryForm>.Success(ToForm(library));
    }

    public async Task<ServiceResult<LibraryForm>> Update(int id, LibraryForm form)
    {
        var library = await _libraries.GetByIdAsync(id);
        if (library == null)
            return ServiceResult<LibraryForm>.NotFound($"Library {id} was not found.");

        library.Name = form.Name;
        library.Location = form.Location;

        var updated = await _libraries.UpdateAsync(library);
        return ServiceResult<LibraryForm>.Success(ToForm(updated));
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var library = await _libraries.GetByIdAsync(id);
        if (library == null)
            return ServiceResult.NotFound($"Library {id} was not found.");

        await _libraries.DeleteAsync(library);
        return ServiceResult.Success();
    }

    private static Library ToModel(LibraryForm form)
    {
        return new Library
        {
            Id = form.Id,
            Name = form.Name,
            Location = form.Location
        };
    }

    private static LibraryForm ToForm(Library library)
    {
        return new LibraryForm
        {
            Id = library.Id,
            Name = library.Name,
            Location = library.Location
        };
    }
}

public interface ILibrariesService
{
    Task<IEnumerable<LibraryForm>> GetAll();

    Task<ServiceResult<LibraryForm>> GetById(int id);

    Task<ServiceResult<LibraryForm>> Add(LibraryForm form);

    Task<ServiceResult<LibraryForm>> Update(int id, LibraryForm form);

    Task<ServiceResult> Delete(int id);
}
