using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Dto.UserDto
{
    public class RegisterUserDto
    {
        public string address { get; set; }

        public string birthday { get; set; }

        public string name { get; set; }

        public string nickname { get; set; }

        public string password { get; set; }
        
        public string surename { get; set; }                      
    }  
}
