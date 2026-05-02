using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SecureNotes.Models
{
    public class Note
    {
        
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 100 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "O conteúdo é obrigatório")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "O conteúdo deve ter entre 5 e 1000 caracteres")]
        
        [RegularExpression(@"^[^<>]*$", ErrorMessage = "HTML não é permitido")]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
        [Required]
        [ValidateNever]
        public string UserId { get; set; } = string.Empty;

        [ValidateNever]
        public ApplicationUser? User { get; set; }

        
    }
}