﻿namespace Finance.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        public string Code;

        protected DomainException(string code, string? message, Exception? innerException) : base(message, innerException)
        {
            Code = code;
        }
    }
}
