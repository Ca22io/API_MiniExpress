using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MiniExpress.Models;

namespace MiniExpress.Models
{
    public class UsuarioModel
    {
        [Key]
        public int IdUsuario { get; set; }

        // --- Chave Estrangeira para Perfil ---
        public int? IdPerfil { get; set; }

        [ForeignKey("IdPerfil"), NotMapped] // Especifica a FK
        public PerfilModel? Perfil { get; set; } // Propriedade de Navegação

        [Required]
        public string? Nome { get; set; }

        [Required, EmailAddress, MaxLength(200)] // O atributo EmailAddress valida o formato do e-mail
        public string? Email { get; set; }

        [Required, MaxLength(11)]
        public string? CPF { get; set; }

        [MaxLength(20)]
        public string? Telefone { get; set; } // O '?' torna a string opcional (pode ser nula)

        public DateTime? DataCadastro { get; set; }

        [Required, MaxLength(128)]
        public string? SenhaHash { get; set; }

        // --- Propriedades de Navegação para outras tabelas ---
        // Um usuário pode ter uma loja
        [NotMapped] // Não mapeia essa propriedade para o banco de dados
        public LojaModel? Loja { get; set; }

        [NotMapped] // Não mapeia essa propriedade para o banco de dados
        public ICollection<EnderecoModel>? Enderecos { get; set; }
    }
}