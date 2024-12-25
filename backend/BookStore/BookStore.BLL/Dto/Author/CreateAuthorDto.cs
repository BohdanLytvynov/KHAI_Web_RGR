using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Dto.Author
{
    public class CreateAuthorDto
    {
        public string Name { get; set; }

        public string Surename { get; set; }

        public DateOnly BirthDate { get; set; }
    }
}
