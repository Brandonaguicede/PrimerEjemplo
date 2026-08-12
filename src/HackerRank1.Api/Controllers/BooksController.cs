using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HackerRank1.BusinessLogic.Services;
using HackerRank1.Entities.DTO;
using HackerRank1.Entities.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace HackerRank1.Api.Controllers
{
    [ApiController]
    [Route("api/libraries/{libraryId}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILibrariesService _librariesService;
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService, ILibrariesService librariesService)
        {
            _librariesService = librariesService;
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int libraryId)
        {
            var library = (await _librariesService.Get(new[] { libraryId })).FirstOrDefault();
            if (library == null)
                return NotFound();

            var books = await _booksService.Get(libraryId, null);
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int libraryId, BookForm form)
        {
            var library = (await _librariesService.Get(new[] { libraryId })).FirstOrDefault();
            if (library == null)
                return NotFound();

            var book = new Book
            {
                Id = form.Id,
                Name = form.Name,
                Category = form.Category ?? string.Empty,
                LibraryId = libraryId
            };

            await _booksService.Add(book);
            return StatusCode(StatusCodes.Status201Created, book);
        }
    }
}
