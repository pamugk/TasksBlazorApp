namespace TasksSpa.Logic
{
    public static class InputValidator
    {
        public static bool LoginIsValid(string login)
        {
            return login != null && login.Trim().Length > 0 && !login.Contains("@");
        }

        public static bool PasswordIsValid(string password)
        {
            return password != null && password.Trim().Length > 0;
        }
    }
}
