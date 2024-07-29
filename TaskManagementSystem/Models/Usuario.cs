using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "Campo nome deve conter no máximo 100 caracteres.")]
        [RegularExpression("^[A-ZÀ-Úa-zà-ú ]+$", ErrorMessage = "Nome deve conter apenas letras.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo email é obrigatório.")]
        [StringLength(100, ErrorMessage = "Campo email deve conter no máximo 100 caracteres.")]
        //[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", 
        //ErrorMessage = "O campo Email não é um endereço de e-mail válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Cargo é obrigatório.")]
        [StringLength(50, ErrorMessage = "Campo cargo deve conter no máximo 50 caracteres.")]
        [RegularExpression(@"[\p{L}\p{M}]+", ErrorMessage = "Deve conter apenas letras.")]
        public string Cargo { get; set; }
    }
}