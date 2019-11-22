using System;

namespace TasksSpa.Services.Exceptions
{
    public class NotAuthorizedException: Exception
    {
        public NotAuthorizedException() { }
        public NotAuthorizedException(string message) : base(message) { }
    }
}