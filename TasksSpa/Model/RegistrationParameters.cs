using System.ComponentModel.DataAnnotations;

namespace TasksSpa.Model
{
    public class RegistrationParams
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Требуется ввести логин")]
        [CustomValidation(typeof(Logic.InputValidator), "LoginIsValid",
            ErrorMessage = "Введённый логин содержит запрещённые символы")]
        public string Login { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Требуется ввести пароль")]
        [CustomValidation(typeof(Logic.InputValidator), "PasswordIsValid",
            ErrorMessage = "Введённый пароль содержит запрещённые символы")]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Требуется ввести повтор пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordRepeat { get; set; }
    }
}
