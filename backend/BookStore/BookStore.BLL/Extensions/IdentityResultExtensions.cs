using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Extensions
{
    public static class IdentityResultExtensions
    {
        public static string GetErrors(this IdentityResult r)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var e in r.Errors.ToList())
            {
                stringBuilder.Append(e.Description);
                stringBuilder.Append(Environment.NewLine);
            }

            return stringBuilder.ToString();
        }
    }
}
