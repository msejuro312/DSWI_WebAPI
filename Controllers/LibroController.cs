using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {

        private readonly ILibroService _service;

        public LibroController(ILibroService service)
        {
            _service = service;
        }

        [HttpGet("listar")]

        public async Task<IActionResult> Get()
        {
            var libros = _service.list();
            return Ok(await Task.Run(() => libros));
        }
    }
}
