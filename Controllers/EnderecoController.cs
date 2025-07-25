using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniExpress.Data;
using MiniExpress.Models;

namespace MiniExpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly BdContext _context;

        public EnderecoController(BdContext context)
        {
            _context = context;
        }

        [HttpGet("{IdUsuario}")]
        public async Task<IActionResult> LocalizarEnderecos(int IdUsuario)
        {
            if (IdUsuario <= 0)
            {
                return BadRequest("ID de usuário inválido.");
            }

            if (!VerificarUsuario(IdUsuario))
            {
                return NotFound("Usuário não encontrado.");
            }

            var enderecos = await _context.Enderecos
                .Where(e => e.IdUsuario == IdUsuario)
                .AsNoTracking()
                .ToListAsync();

            return Ok(enderecos);

        }

        [HttpPost]
        public async Task<IActionResult> AdicionarEndereco([FromBody] EnderecoModel endereco)
        {
            if (ModelState.IsValid)
            {
                if (endereco.IdUsuario != null || endereco.IdLoja != null)
                {
                    if (VerificarUsuario(endereco.IdUsuario.Value))
                    {
                        if (!VerificarEnderecoUsuario(endereco.IdUsuario.Value))
                        {
                            endereco.Principal = "true";
                        }
                        else
                        {
                            endereco.Principal = "false";
                        }

                        _context.Enderecos.Add(endereco);

                        if (await _context.SaveChangesAsync() > 0)
                        {
                            return Ok("Endereço adicionado com sucesso.");
                        }

                        return BadRequest("Erro ao adicionar endereço.");

                    }

                    if (VerificarLoja(endereco.IdLoja.Value))
                    {
                        endereco.Principal = "true";

                        _context.Enderecos.Add(endereco);

                        if (await _context.SaveChangesAsync() > 0)
                        {
                            return Ok("Endereço adicionado com sucesso.");
                        }

                        return BadRequest("Erro ao adicionar endereço.");
                    }

                    return NotFound("Usuário ou loja não encontrado.");

                }
                else
                {
                    return BadRequest("Deve ser passado um id de usuario ou loja.");
                }
            }

            return BadRequest("Dados inválidos.");
        }

        [HttpDelete("{IdEndereco}/{IdUsuario}")]
        public async Task<IActionResult> ExcluirEndereco(int IdEndereco, int IdUsuario)
        {
            if (IdEndereco > 0 && IdUsuario > 0)
            {
                if (VerificarUsuario(IdUsuario) && VerificarEndereco(IdEndereco))
                {
                    var ObterEndereco = await _context.Enderecos
                        .FirstOrDefaultAsync(e => e.IdEndereco == IdEndereco && e.IdUsuario == IdUsuario);

                    if (ObterEndereco.Principal == "true")
                    {
                        _context.Enderecos.Remove(ObterEndereco);

                        if (await _context.SaveChangesAsync() > 0)
                        {
                            var SegundoEndereco = await _context.Enderecos
                            .Where(e => e.IdUsuario == IdUsuario && e.Principal == "false")
                            .FirstOrDefaultAsync();

                            if (SegundoEndereco == null)
                            {
                                return Ok("Endereço principal excluído com sucesso. Sem endereços restantes.");
                            }

                            SegundoEndereco.Principal = "true";

                            _context.Enderecos.Update(SegundoEndereco);

                            if (await _context.SaveChangesAsync() > 0)
                            {
                                return Ok("Endereço principal excluído com sucesso. Um novo endereço principal foi definido.");
                            }
                        }

                        return BadRequest("Erro ao excluir endereço principal.");

                    }
                    
                    _context.Enderecos.Remove(ObterEndereco);

                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return Ok("Endereço excluído com sucesso.");
                    }

                    return BadRequest("Erro ao excluir endereço.");
                }

                return NotFound("Usuário ou endereço não encontrado.");
            }

            return BadRequest("ID de endereço ou usuário inválido.");
        }

        private bool VerificarEndereco(int IdEndereco)
        {
            return _context.Enderecos.Any(e => e.IdEndereco == IdEndereco);
        }

        private bool VerificarUsuario(int IdUsuario)
        {
            return _context.Usuarios.Any(u => u.IdUsuario == IdUsuario);
        }

        private bool VerificarLoja(int IdLoja)
        {
            return _context.Lojas.Any(l => l.IdLoja == IdLoja);
        }

        private bool VerificarEnderecoUsuario(int IdUsuario)
        {
            return _context.Enderecos.Any(e => e.IdUsuario == IdUsuario);
        }

    }
}