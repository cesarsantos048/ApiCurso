using System.ComponentModel.DataAnnotations;

namespace Api.Dto
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O Campo {0} está no formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public string UserName { get; set; }

    }
}
