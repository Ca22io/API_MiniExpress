using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniExpress.Models
{
    public class EnderecoModel
    {
        [Key]
        public int IdEndereco { get; set; }

        public int IdUsuario { get; set; } // Chave estrangeira para o usuário

        [ForeignKey("IdUsuario"), NotMapped] // Especifica a FK
        public UsuarioModel? Usuario { get; set; } // Propriedade de Navegação

        public int IdLoja { get; set; } // Chave estrangeira para a loja (opcional)

        [ForeignKey("IdLoja"), NotMapped] // Especifica a FK
        public LojaModel? Loja { get; set; } // Propriedade de Navegação

        [MaxLength(100)]
        public string? Logradouro { get; set; } // Ex: Rua, Avenida, etc.

        [MaxLength(10)]
        public string? Numero { get; set; } // Número da residência ou estabelecimento

        [MaxLength(100)]
        public string? Complemento { get; set; } // Ex: Apartamento, Bloco, etc.

        [MaxLength(100)]
        public string? Bairro { get; set; } // Bairro

        [MaxLength(100)]
        public string? Cidade { get; set; } // Cidade

        [MaxLength(2)]
        public string? Estado { get; set; } // Estado (UF)

        [MaxLength(10)]
        public string? CEP { get; set; } // Código Postal
    }
}