using BookStore.BLL.Dto.LogedOut;
using BookStore.BLL.Exceptions.AccountExceptions;
using BookStore.BLL.Services.CookieServices.Interfaces;
using BookStore.BLL.Services.TokenServices.Interfaces;
using BookStore.DAL.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Account.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result<LogedOutDto>>
    {       
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
       
        public LogoutCommandHandler(     
            UserManager<User> userManager,
            IHttpContextAccessor contextAccessor,
            ITokenService tokenService)
        {                 
            _contextAccessor = contextAccessor;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<Result<LogedOutDto>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _contextAccessor.HttpContext.Items["User"] as User;

                if (user is null)
                    throw new ArgumentNullException("Fail to get user from the HTTPContext");

                if (!_contextAccessor.HttpContext.Request.Cookies.TryGetValue("accessToken", out var token))
                    throw new InvalidTokenException("Access Token");

                var tokensJti = _tokenService.GetUserClaimFromAccessToken(token, JwtRegisteredClaimNames.Jti);

                if (tokensJti is null)
                    throw new Exception("Fail to get Token jti!");

                var idToRemove = user.AccessTokenIds.
                    FirstOrDefault(x => x.AccessTokenGUID.Equals(Guid.Parse(tokensJti)));

                user.AccessTokenIds.Remove(idToRemove);

                await _userManager.UpdateAsync(user);
                
                return FluentResults.Result.Ok(new LogedOutDto() { Message = "Loged out!" });
                    
            }
            catch (Exception e)
            {
                return FluentResults.Result.Fail(e.Message);                
            }
        }
    }
}
