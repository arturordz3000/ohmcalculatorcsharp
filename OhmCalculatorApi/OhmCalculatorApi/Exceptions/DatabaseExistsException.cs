using System;
namespace OhmCalculatorApi.Exceptions
{
    public class DatabaseExistsException : Exception
    {
        public DatabaseExistsException()
            : base("Database already exists.")
        {
        }
    }
}
