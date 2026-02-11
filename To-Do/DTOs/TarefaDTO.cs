using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using To_Do.Models.Enums;

namespace To_Do.DTOs
{
    public class TarefaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Categoria? Categoria { get; set; }
        public DateTime? Data { get; set; }
        public Status? Status { get; set; }
    }
}