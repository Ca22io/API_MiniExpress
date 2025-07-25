using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniExpress.Data;
using MiniExpress.Models;
using MiniExpress.Dto.Usuario;

namespace MiniExpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly BdContext _context;
        public UsuarioController(BdContext context)
        {
            _context = context;
        }

        [HttpGet("{idUsuario?}")]
        public async Task<IActionResult> ObterUsuarios(int? idUsuario)
        {
            if (idUsuario.HasValue)
            {

                if (!UsuarioExiste(idUsuario.Value))
                {
                    return NotFound("Usuário não encontrado.");
                }

                var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.IdUsuario == idUsuario.Value);
                return Ok(usuario);
                
            }

            var usuarios = await _context.Usuarios.AsNoTracking().ToListAsync();
            return Ok(usuarios);
            
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                if (usuario.IdUsuario > 0 && UsuarioExiste(usuario.IdUsuario))
                {
                    return BadRequest("Usuário já existe.");
                }
                else
                {
                    var VerificarPerfil = usuario.IdPerfil == null ? 1 : usuario.IdPerfil;

                    usuario.IdPerfil = VerificarPerfil;

                    usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.SenhaHash);

                    usuario.DataCadastro = DateTime.Now;

                    _context.Usuarios.Add(usuario);
                    
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return Ok("Usuario criado com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Erro ao criar o usuário.");
                    }
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
                    
            
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarUsuario( [FromBody] AtualizarUsuarioDto usuario)
        {
            if (ModelState.IsValid)
            {
                if (UsuarioExiste(usuario.IdUsuario))
                {
                    var localizarUsuario = await _context.Usuarios.FindAsync(usuario.IdUsuario);

                    localizarUsuario.IdPerfil = usuario.IdPerfil == null ? localizarUsuario.IdPerfil : usuario.IdPerfil;
                    localizarUsuario.Nome = usuario.Nome == null ? localizarUsuario.Nome : usuario.Nome;
                    localizarUsuario.Email = usuario.Email == null ? localizarUsuario.Email : usuario.Email;
                    localizarUsuario.CPF = usuario.CPF == null ? localizarUsuario.CPF : usuario.CPF;
                    localizarUsuario.Telefone = usuario.Telefone == null ? localizarUsuario.Telefone : usuario.Telefone;

                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return Ok("Usuario atualizado com sucesso!");
                    }
                    else
                    {
                        return BadRequest("Erro ao atualizar o usuário.");
                    }
                }
                else
                {
                    return NotFound("Usuário não encontrado.");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        [HttpPatch]
        public async Task<IActionResult> AtualizarSenha([FromBody] AtualizarSenhaDto atualizarSenhaDto)
        {
            if (UsuarioExiste(atualizarSenhaDto.IdUsuario))
            {
                
                var usuario = await _context.Usuarios.FindAsync(atualizarSenhaDto.IdUsuario);

                usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(atualizarSenhaDto.NovaSenha);

                _context.Update(usuario);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok("Senha atualizada com sucesso!");
                }
                else
                {
                    return BadRequest("Erro ao atualizar a senha.");
                }
            }
            else
            {
                return NotFound("Usuário não encontrado.");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            if (UsuarioExiste(id))
            {
                var usuario = await _context.Usuarios.FindAsync(id);

                _context.Usuarios.Remove(usuario);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok("Usuário excluído com sucesso!");
                }
                else
                {
                    return BadRequest("Erro ao excluir o usuário.");
                }
            }
            else
            {
                return NotFound("Usuário não encontrado.");
            }
        }

        private bool UsuarioExiste(int id)
        {
            return _context.Usuarios.Any(x => x.IdUsuario == id);
        }

    }
}