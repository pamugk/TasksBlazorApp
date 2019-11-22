using System.ComponentModel.DataAnnotations;

namespace TasksSpa.Model
{
    public class LoginParams
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Требуется ввести логин")]
        public string Login { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Требуется ввести пароль")]
        public string Password { get; set; }
    }
}