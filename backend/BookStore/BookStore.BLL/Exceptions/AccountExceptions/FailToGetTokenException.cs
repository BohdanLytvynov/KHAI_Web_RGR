using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Exceptions.AccountExceptions
{
    internal class FailToGetTokenException : Exception
    {
        public FailToGetTokenException() : base("Error occured when trying to get Access Token!")
        {

        }
    }
}
