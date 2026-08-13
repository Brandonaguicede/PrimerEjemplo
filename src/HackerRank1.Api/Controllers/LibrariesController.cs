using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HackerRank1.Application.Common;
using HackerRank1.Application.DTO;
using HackerRank1.Application.Services;

namespace HackerRank1.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrariesController : ControllerBase
    {
        private readonly ILibrariesService _librariesService;

        public LibrariesController(ILibrariesService librariesService)
        {
            _librariesService = librariesService;
        }

        [HttpGet]        
        public async Task<IActionResult> GetAll()
        {
            var libraries = await _librariesService.GetAll();
            return Ok(libraries);
        }

        [HttpGet("{libraryId}")]
        public async Task<IActionResult> Get(int libraryId)
        {
            var result = await _librariesService.GetById(libraryId);
            if (result.Status == ServiceResultStatus.NotFound)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpPost]        
        public async Task<IActionResult> Add(LibraryForm form)
        {
            var result = await _librariesService.Add(form);
            return Ok(result.Value);
        }

        [HttpPut("{libraryId}")]
        public async Task<IActionResult> Update(int libraryId, LibraryForm form)
        {
            var result = await _librariesService.Update(libraryId, form);
            if (result.Status == ServiceResultStatus.NotFound)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{libraryId}")]
        public async Task<IActionResult> Delete(int libraryId)
        {
            var result = await _librariesService.Delete(libraryId);
            if (result.Status == ServiceResultStatus.NotFound)
                return NotFound();

            return NoContent();
        }
    }
}
