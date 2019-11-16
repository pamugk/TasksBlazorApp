using System.ComponentModel.DataAnnotations;

namespace TasksSpa.Logic
{
    public static class InputValidator
    {
        public static ValidationResult LoginIsValid(string login)
        {
            return login != null && login.Trim().Length > 0 && !login.Contains("@") ? 
                ValidationResult.Success : 
                new ValidationResult(null);
        }

        public static ValidationResult PasswordIsValid(string password)
        {
            return password != null && password.Trim().Length > 0 && !password.Contains("@") ?
                ValidationResult.Success :
                new ValidationResult(null);
        }

        public static ValidationResult TaskTitleIsValid(string title)
        {
            return title != null && title.Trim().Length > 0 && !title.Contains("@") ?
                ValidationResult.Success :
                new ValidationResult(null);
        }
    }
}
