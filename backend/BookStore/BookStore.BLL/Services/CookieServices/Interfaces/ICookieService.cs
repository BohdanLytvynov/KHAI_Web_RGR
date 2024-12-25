using Microsoft.AspNetCore.Http;

namespace BookStore.BLL.Services.CookieServices.Interfaces
{
    public interface ICookieService
    {
        Task ClearRequestCookiesAsync(HttpContext httpContext);

        Task AppendCookiesToResponseAsync(HttpResponse httpResponse, params (string key, string value, CookieOptions options)[] values);
    }
}
