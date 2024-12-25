using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Exceptions.AccountExceptions
{
    internal class IncorrectAccessToken : Exception
    {
        public IncorrectAccessToken() : base("Incorrect accessToken!") 
        {
            
        }
    }
}
