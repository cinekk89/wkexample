using System;

namespace WKExample.Shared.Exceptions
{
    public class WKDomainException : Exception
    {
        public WKDomainException(string message)
            :base(message)
        {
        }
    }
}
