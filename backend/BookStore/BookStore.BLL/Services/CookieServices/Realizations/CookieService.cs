using BookStore.BLL.Services.CookieServices.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Services.CookieServices.Realizations
{
    public class CookieService : ICookieService
    {
        public async Task AppendCookiesToResponseAsync(HttpResponse httpResponse, params (string key, string value, CookieOptions options)[] values)
        {
            await Task.Run(() =>
            {
                foreach (var cookie in values)
                {
                    httpResponse.Cookies.Append(cookie.key, cookie.value, cookie.options);
                }
            });
        }

        public async Task ClearRequestCookiesAsync(HttpContext httpContext)
        {
            await Task.Run(() =>
            {
                foreach (var cookie in httpContext!.Request.Cookies.Keys)
                {
                    httpContext!.Response.Cookies.Delete(cookie, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(-1),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None                        
                    });
                }
            });

            await Task.CompletedTask;
        }
    }
}
