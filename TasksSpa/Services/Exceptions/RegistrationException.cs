using System;

namespace TasksSpa.Services.Exceptions
{
    public class RegistrationException: Exception
    {
        public RegistrationException() { }
        public RegistrationException(string message) : base(message) { }
    }
}
