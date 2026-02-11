using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace To_Do.DTOs
{
    public class ContaDTO
    {
        [Required(ErrorMessage = "O campo email é obrigatório"), StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório"), StringLength(20, MinimumLength = 6)]
        public string Senha { get; set; }
    }
}