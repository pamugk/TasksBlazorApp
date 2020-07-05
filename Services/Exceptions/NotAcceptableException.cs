using System;

namespace TodosSpa.Services.Exceptions
{
    public class NotAcceptableException:Exception
    {
        public NotAcceptableException() { }
        public NotAcceptableException(string message): base(message) { }
    }
}