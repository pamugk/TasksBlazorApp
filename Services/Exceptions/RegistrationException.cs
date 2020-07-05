using System;

namespace TodosSpa.Services.Exceptions
{
    public class RegistrationException: Exception
    {
        public RegistrationException() { }
        public RegistrationException(string message) : base(message) { }
    }
}
