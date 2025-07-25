using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniExpress.Dto.Usuario
{
    public class AtualizarUsuarioDto
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "IdUsuario é obrigatório e deve ser um número positivo.")]
        public required int IdUsuario { get; set; }

        public int? IdPerfil { get; set; }

        public string? Nome { get; set; }

        [EmailAddress, MaxLength(200)] 
        public string? Email { get; set; }

        [MaxLength(11)]
        public string? CPF { get; set; }

        [MaxLength(20)]
        public string? Telefone { get; set; }
    }
}