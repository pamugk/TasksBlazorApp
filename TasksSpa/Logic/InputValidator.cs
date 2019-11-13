namespace TasksSpa.Logic
{
    public class InputValidator
    {
        public bool LoginIsValid(string login)
        {
            return login != null && login.Trim().Length > 0 && !login.Contains("@");
        }

        public bool PasswordIsValid(string password)
        {
            return password != null && password.Trim().Length > 0 && !password.Contains("@");
        }

        public bool TaskTitleIsValid(string title)
        {
            return title != null && title.Trim().Length > 0 && !title.Contains("@");
        }
    }
}
