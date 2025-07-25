using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniExpress.Data;
using MiniExpress.Models;

namespace MiniExpress.Controllers
{
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
                            endereco.Principal = true;
                        }

                        endereco.Principal = false;

                        _context.Enderecos.Add(endereco);

                        if (await _context.SaveChangesAsync() > 0)
                        {
                            return Ok("Endereço adicionado com sucesso.");
                        }

                        return BadRequest("Erro ao adicionar endereço.");

                    }

                    if (VerificarLoja(endereco.IdLoja.Value))
                    {
                        endereco.Principal = true;

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