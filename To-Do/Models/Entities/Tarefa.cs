using System;
using System.ComponentModel.DataAnnotations;
using To_Do.Models.Enums;

namespace To_Do.Models.Entities
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Título é obrigatório"), StringLength(100)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo Título é obrigatório"), StringLength(4000)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo Categoria é obrigatório")]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "O campo Data de realização é obrigatório")]
        public DateTime Data { get; set; }
        public Status Status { get; set; } = Status.Pendente;
        public DateTime CreatedAT { get; set; } = DateTime.Now;

    }
}