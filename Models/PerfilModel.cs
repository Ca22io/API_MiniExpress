using System.ComponentModel.DataAnnotations;

namespace MiniExpress.Models
{
    public class PerfilModel
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string? NomePerfil { get; set; }
    }
}