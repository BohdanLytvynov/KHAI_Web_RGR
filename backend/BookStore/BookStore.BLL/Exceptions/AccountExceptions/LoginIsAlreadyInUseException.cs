using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Exceptions.AccountExceptions
{
    internal class LoginIsAlreadyInUseException : Exception
    {
        public LoginIsAlreadyInUseException() : base("Login is already in use!")
        {
            
        }
    }
}
