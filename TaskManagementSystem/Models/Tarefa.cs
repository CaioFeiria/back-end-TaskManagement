using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo titulo é obrigatório.")]
        [StringLength(100, ErrorMessage = "Campo titulo deve conter no máximo 100 caracteres.")]
        [RegularExpression("^[A-ZÀ-Úa-zà-ú ]+$", ErrorMessage = "Nome deve conter apenas letras.")]
        public string Titulo { get; set; }

        [StringLength(450, ErrorMessage = "Campo descrição deve conter no máximo 450 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo prazo é obrigatório.")]
        public DateTime Prazo { get; set; }

        public bool Prioridade { get; set; }

        [Required(ErrorMessage = "Campo estado é obrigatório.")]
        public bool Estado { get; set; }

        [Required(ErrorMessage = "Campo responsavel é obrigatório.")]
        public int Id_responsavel { get; set; }
    }
}