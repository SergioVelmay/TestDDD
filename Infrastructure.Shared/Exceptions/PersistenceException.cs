using System;
namespace Infrastructure.Shared.Exceptions
{
    public class PersistenceException : Exception
    {
        public PersistenceException()
        {
        }

        public PersistenceException(string message) : base(message)
        {

        }
    }
}
