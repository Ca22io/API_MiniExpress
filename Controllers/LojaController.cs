using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniExpress.Data;

namespace MiniExpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LojaController : ControllerBase
    {
        private readonly BdContext _context;
        public LojaController(BdContext context)
        {
            _context = context;
        }

        [HttpGet("{idLoja?}")]
        public async Task<ActionResult> Obterlojas(int? idLoja)
        {
            if (idLoja.HasValue)
            {
                if (idLoja <= 0)
                {
                    return BadRequest("ID da loja inválido.");
                }

                if (!LojaExiste(idLoja.Value))
                {
                    return NotFound("Loja não encontrada.");
                }

                var loja = await _context.Lojas.AsNoTracking().FirstOrDefaultAsync(l => l.IdLoja == idLoja);
                return Ok(loja);

            }
            
            var Lojas = await _context.Lojas.AsNoTracking().ToListAsync();
            return Ok(Lojas);
        }

        [HttpPost]
        public async Task<IActionResult> CriarLoja([FromBody] Models.LojaModel Loja)
        {
            if (ModelState.IsValid)
            {
                bool LocalizaUsuario = await _context.Usuarios.AnyAsync(u => u.IdUsuario == Loja.IdUsuario);

                if (!LocalizaUsuario)
                {
                    return BadRequest("Usuário não encontrado.");
                }

                if (UsuarioLojaExiste(Loja.IdUsuario))
                {
                    return BadRequest("Usuário já possui uma loja.");
                }
                
                var usuario = await _context.Usuarios.FindAsync(Loja.IdUsuario);

                if (usuario.IdPerfil != 2)
                {
                    return BadRequest("Usuário não é um Lojista.");
                }
        
                Loja.DataCadastro = DateTime.Now;
                _context.Lojas.Add(Loja);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok("Loja criada com sucesso.");
                }
                
            }

            return BadRequest("Dados inválidos.");

        }

        [HttpPut]
        public async Task<IActionResult> AtualizarLoja([FromBody] Models.LojaModel Loja)
        {
            if (ModelState.IsValid)
            {

                if (!LojaExiste(Loja.IdLoja))
                {
                    return NotFound("Loja não encontrada ou ID inválido.");
                }

                _context.Entry(Loja).State = EntityState.Modified;

                _context.Entry(Loja).Property(l => l.DataCadastro).IsModified = false;

                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok("Loja atualizada com sucesso.");
                }
            }

            return BadRequest("Dados inválidos.");
        }

        [HttpDelete("{idLoja}")]
        public async Task<IActionResult> DeletarLoja(int idLoja)
        {
            if (idLoja > 0 && LojaExiste(idLoja))
            {
                var loja = await _context.Lojas.FindAsync(idLoja);

                _context.Lojas.Remove(loja);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok("Loja deletada com sucesso.");
                }
            }

            return BadRequest("A loja não existe ou ID inválido.");
        }

        private bool LojaExiste(int idLoja)
        {
            return _context.Lojas.Any(l => l.IdLoja == idLoja);
        }
        
        private bool UsuarioLojaExiste(int idUsuario)
        {
            return _context.Lojas.Any(l => l.IdUsuario == idUsuario);
        }

    }
}