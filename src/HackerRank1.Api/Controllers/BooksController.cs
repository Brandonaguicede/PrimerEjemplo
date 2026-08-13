using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HackerRank1.Application.Common;
using HackerRank1.Application.DTO;
using HackerRank1.Application.Services;

namespace HackerRank1.Api.Controllers
{
    [ApiController]
    [Route("api/libraries/{libraryId}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int libraryId)
        {
            var result = await _booksService.Get(libraryId, null);
            if (result.Status == ServiceResultStatus.NotFound)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int libraryId, BookForm form)
        {
            var result = await _booksService.Add(libraryId, form);
            if (result.Status == ServiceResultStatus.NotFound)
                return NotFound();

            return Created($"/api/libraries/{libraryId}/books/{result.Value!.Id}", result.Value);
        }
    }
}
