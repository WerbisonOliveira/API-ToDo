using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace To_Do.Models.Entities
{
    public class Conta
    {
        [Required(ErrorMessage = "O campo email é obrigatório"), StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório"), StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório"), StringLength(20, MinimumLength = 6)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo confirmar senha é obrigatório"), StringLength(20, MinimumLength = 6)]
        [Compare("Senha")]
        public string ConfirmarSenha { get; set; }
    }
}