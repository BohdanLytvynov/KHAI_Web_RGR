using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Exceptions.AccountExceptions
{
    internal class IdentityException : Exception
    {
        public IdentityException(string msg) : base(msg) 
        { 
            
        }
    }
}
