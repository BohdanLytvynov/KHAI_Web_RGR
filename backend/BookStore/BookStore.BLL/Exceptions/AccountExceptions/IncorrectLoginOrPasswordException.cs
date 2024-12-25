using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Exceptions.AccountExceptions
{
    internal class IncorrectLoginOrPasswordException : Exception
    {
        public IncorrectLoginOrPasswordException() : base("Incorrect Login or/and Password") 
        {
            
        }
    }
}
