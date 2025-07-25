using System.ComponentModel.DataAnnotations;

namespace MiniExpress.Dto.Usuario
{
    public class AtualizarSenhaDto
    {
        [Required]
        public int IdUsuario { get; set; }

        [Required, MinLength(6)]
        public string NovaSenha { get; set; }
    }
}