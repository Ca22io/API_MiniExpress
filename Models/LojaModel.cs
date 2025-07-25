using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniExpress.Models
{
    public class LojaModel
    {
        [Key]
        public int IdLoja { get; set; } // Chave Primária

        [Required]
        public int IdUsuario { get; set; } // Chave Estrangeira para o usuário
        
        [ForeignKey("IdUsuario"), NotMapped] // Especifica a FK
        public UsuarioModel? Usuario { get; set; } // Propriedade de Navegação para o usuário

        [Required, MaxLength(128)]
        public string? Nome { get; set; } // Nome da loja

        [Required, MaxLength(14)]
        public string? CNPJ { get; set; } // CNPJ da loja

        [Required, MaxLength(20)]
        public string? Telefone { get; set; } // Telefone da loja

        [EmailAddress]
        public string? Email { get; set; } // E-mail da loja

        public DateTime DataCadastro { get; set; } // Data de cadastro da loja
    }
}