
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;

namespace webapi.Controllers
{

    /// <summary>
    /// Apenas executa uma busca no banco de dados para 
    /// verificar se as migrations e seeders foram aplicados
    /// corretamente.
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class BuscaController : ControllerBase
    {
        private readonly PostgreSQLContext _context;

        public BuscaController(PostgreSQLContext context)
        {
            _context = context;
        }

        [Route("usuarios")]
        [HttpGet]
        public async Task<IActionResult> GetUsuarios() => Ok(await _context.Usuarios.ToListAsync());

        [Route("produtos")]
        [HttpGet]
        public async Task<IActionResult> GetProdutos() => Ok(await _context.Produtos.ToListAsync());
 
    }
}