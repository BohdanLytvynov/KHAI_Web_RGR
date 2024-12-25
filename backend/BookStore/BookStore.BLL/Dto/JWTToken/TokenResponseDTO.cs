using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Dto.JWTToken
{
    public class TokenResponseDTO
    {
        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpDate { get; set; }
    }
}
