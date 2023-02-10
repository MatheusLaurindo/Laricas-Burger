using System.ComponentModel.DataAnnotations;

namespace LanchesMac.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Informe o nome de Usuário")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirme sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        public string? ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; }
    }
}
