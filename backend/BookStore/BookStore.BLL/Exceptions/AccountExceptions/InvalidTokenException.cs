using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Exceptions.AccountExceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(string tokenName) : base($"Invalid {tokenName}")
        {

        }
    }
}
