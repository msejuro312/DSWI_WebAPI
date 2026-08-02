using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

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

        [HttpGet("{LibroId}")]

        public async Task<IActionResult> GetById(int LibroId)
        {
            var libro = _service.getById(LibroId);
            if (libro == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    message = "No se encontré el libro con el id " + LibroId,
                    success = false,
                    data = null
                });
            }
            else
            {
                return Ok(new ApiResponse<Libro>
                {
                    message = "Libro Encontrado",
                    success = true,
                    data = libro
                });
            }
            
        }
    }
}
