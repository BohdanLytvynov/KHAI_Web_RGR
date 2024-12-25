using BookStore.BLL.Services.TokenServices.Interfaces;
using BookStore.BLL.Services.TokenServices.Realizations;
using BookStore.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.WebApi.MiddleWares
{
    public class JWTMiddleware 
    {
        private readonly RequestDelegate _requestDelegate;             
        private readonly JWTTokenConfiguration _jwtTokenConfiguration;
      
        public JWTMiddleware(RequestDelegate requestDelegate,            
            JWTTokenConfiguration jWTTokenConfiguration)
        {
            _requestDelegate = requestDelegate;            
            _jwtTokenConfiguration = jWTTokenConfiguration;                      
        }

        public async Task Invoke(HttpContext context, UserManager<User> userManager, 
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            try
            {
                // Get Current JWT
                context.Request.Cookies.TryGetValue("accessToken", out var token);
                // JWT not found throw error
                if (token is null)
                    throw new Exception("No token in cookie!");
                //AutoValidate JWT (Configure)
                var tokenHandler = new JwtSecurityTokenHandler();

                byte[] key = Encoding.ASCII.GetBytes
                (_jwtTokenConfiguration.SecretKey);

                var tokenValidationParams = new
                TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new
                    SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtTokenConfiguration.Issuer,
                    ValidAudience = _jwtTokenConfiguration.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
                //Vallidate JWT
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParams, out var validatedToken);
                var jwtToken =
                (JwtSecurityToken)validatedToken;

                if (claimsPrincipal is null)
                    throw new Exception("Invalid Token!");

                var userId = claimsPrincipal
                    .FindFirstValue(ClaimTypes.NameIdentifier);

                //Find user to be attached to httpContext
                var user = await userManager.Users.Where(x => x.Id.Equals(Guid.Parse(userId)))
                    .Include(x => x.AccessTokenIds).Select(x => x).FirstOrDefaultAsync();


                //Check accessToken Id

                var jti = claimsPrincipal.Claims
                    .FirstOrDefault(x => x.Type.Equals(JwtRegisteredClaimNames.Jti))!.Value;

                if (user.AccessTokenIds.Where(x => x.AccessTokenGUID.Equals(Guid.Parse(jti)))
                    .Select(x => x.AccessTokenGUID).Count() == 0)//No accessToken Id!
                {
                    throw new Exception("Invalid Token!");
                }

                //Check User Role

                var role = claimsPrincipal.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role))!.Value;

                var roleCorrect = await userManager.IsInRoleAsync(user, role);
                if (!roleCorrect)
                    throw new Exception("Invalid Token!");


                //All checks have been passed!
                context.Items["Role"] = role;
                context.Items["User"] = user;
            }
            catch (Exception any) { }
            finally
            {
                await _requestDelegate?.Invoke(context);
            }
            
        }
    }
}
