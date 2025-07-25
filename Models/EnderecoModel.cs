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

        public string? Logradouro { get; set; } // Ex: Rua, Avenida, etc.
        
        public string? Numero { get; set; } // Número da residência ou estabelecimento
        
        public string? Complemento { get; set; } // Ex: Apartamento, Bloco, etc.
        
        public string? Bairro { get; set; } // Bairro
        
        public string? Cidade { get; set; } // Cidade
        
        public string? Estado { get; set; } // Estado (UF)
        
        public string? CEP { get; set; } // Código Postal
    }
}