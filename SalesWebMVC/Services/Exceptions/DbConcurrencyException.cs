using System.Security.Cryptography.X509Certificates;

namespace SalesWebMVC.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException
    {
        public DbConcurrencyException(string message) : base(message)
        {
            
        }

      
    }
}
