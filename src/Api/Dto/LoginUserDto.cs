using System.ComponentModel.DataAnnotations;

namespace Api.Dto
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precia ter entra {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

    }
}
