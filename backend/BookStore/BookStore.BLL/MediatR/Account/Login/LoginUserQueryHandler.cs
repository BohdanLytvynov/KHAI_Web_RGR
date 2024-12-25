using AutoMapper;
using BookStore.BLL.Converters;
using BookStore.BLL.Dto.UserDto;
using BookStore.BLL.Exceptions.AccountExceptions;
using BookStore.BLL.Services.AccessTokenCleaner.Interfaces;
using BookStore.BLL.Services.CookieServices.Interfaces;
using BookStore.BLL.Services.CookieServices.Realizations;
using BookStore.BLL.Services.TokenServices.Interfaces;
using BookStore.BLL.Services.TokenServices.Realizations;
using BookStore.DAL.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.MediatR.Account.Login
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<AuthResponseDto>>
    {
        private readonly UserManager<User> _usermanager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICookieService _cookieService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly JWTTokenConfiguration _jwtTokenConfiguration;
        private readonly DateTimeToDateTimeOffsetConverter _toDateTimeOffsetConverter;
        private readonly ICleaner _Cleaner;

        public LoginUserQueryHandler(UserManager<User> userManager,
            SignInManager<User> signInManager,
            JWTTokenConfiguration jwtTokenConfiguration,
            IHttpContextAccessor contextAccessor,
            ICookieService cookieService,
            ITokenService tokenService,
            IMapper mapper,
            ICleaner cleaner)
        {
            _usermanager = userManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
            _tokenService = tokenService;
            _cookieService = cookieService;
            _jwtTokenConfiguration = jwtTokenConfiguration;
            _mapper = mapper;
            _toDateTimeOffsetConverter = new DateTimeToDateTimeOffsetConverter();
            _Cleaner = cleaner;
        }

        public async Task<Result<AuthResponseDto>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //Main Part of user filtering! 
                var user = await _usermanager
                    .Users.Where(x => x.UserName!.Equals(request.Dto.nickname))
                    .Include(x =>x.AccessTokenIds)
                    .FirstOrDefaultAsync();
                    
                if (user is null)
                {
                    throw new IncorrectLoginOrPasswordException();
                }

                await _signInManager.SignOutAsync();

                var r = await _signInManager.CheckPasswordSignInAsync(user, request.Dto.password, false);

                if (!r.Succeeded)
                {
                    throw new IncorrectLoginOrPasswordException();
                }
                //Main Part of user filtering! 
                var newTokenGuid = Guid.NewGuid();

                var tokenExists = _contextAccessor.HttpContext.Request.Cookies.
                    TryGetValue("accessToken", out var token);

                var creation = DateTime.UtcNow;
                var expiration = creation.AddMinutes(_jwtTokenConfiguration.AccessTokenExpirationMinutes);

                //If there is no Token in the Cookie (Case when user deleted his cookie)
                //we need to add new accessToken Id,
                //clear all the expired cookies
                //cause we will return new cookie to User
                if (!tokenExists)
                {
                    //There is no AccessToken for this user
                    user.AccessTokenIds.Add(new AccessTokenId() 
                    { User = user, AccessTokenGUID = newTokenGuid,
                    ExpDate = expiration});

                    //Do Expired Token Id Cleaning
                    await _Cleaner.Clean(user, creation);

                }
                else //There is the token in the Cookie and in the db(Case when user loged in and not loged out,
                     //but his accessToken Expired!)
                {
                    //Update token in DB

                    //Get Current token from Cookie
                    var jti = _tokenService
                        .GetUserClaimFromAccessToken(token, claimName: JwtRegisteredClaimNames.Jti);

                    if (string.IsNullOrEmpty(jti)) throw new Exception("Fail to get Data from accessToken!");

                    //Find old Token                                
                    var oldtoken = user.AccessTokenIds.
                        FirstOrDefault(x => x.AccessTokenGUID.Equals(Guid.Parse(jti)));
                    //if there is already token in DB - update it
                    if (oldtoken is not null)
                    {
                        //Remove old Token
                        user.AccessTokenIds.Remove(oldtoken);

                        await _usermanager.UpdateAsync(user);
                        //Set new token
                        user.AccessTokenIds.Add(new AccessTokenId() 
                        { User = user, AccessTokenGUID = newTokenGuid, ExpDate = expiration });
                    }
                    else //There is no tokenId in db. Case when user loged out.
                    {
                        //Add new TokenId to db
                        user.AccessTokenIds.Add(new AccessTokenId() 
                        { User = user, AccessTokenGUID = newTokenGuid,
                        ExpDate = expiration });
                    }
                }
                              
                //Generate new JWT Access Token
                var tokenDto = await _tokenService.GenerateAccesToken(user, claims =>
                {
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, newTokenGuid.ToString()));
                    claims.Add(new Claim(JwtRegisteredClaimNames.Exp,
                       expiration.ToString(CultureInfo.InvariantCulture))); //Date of token's expiration
                    claims.Add(new Claim(JwtRegisteredClaimNames.Iat,
                        creation.ToString(CultureInfo.InvariantCulture))); //Date of token's generation
                });

                if (tokenDto is null)
                    throw new Exception("Fail to generate Access Token!");

                //Add Access Toke to Cookie

                await _cookieService.AppendCookiesToResponseAsync(
                    _contextAccessor!.HttpContext!.Response,
                    ("accessToken", tokenDto.AccessToken, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddMinutes(_jwtTokenConfiguration.AccessTokenExpirationMinutes),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        IsEssential = true,
                        Domain = $"{_contextAccessor.HttpContext.Request.Host.Host}",
                        Path = "/"
                    }));
                
                await _usermanager.UpdateAsync(user);

                var responce = _mapper.Map<AuthResponseDto>(user);
                
                return FluentResults.Result.Ok(responce);

            }
            catch (Exception e)
            {
                return FluentResults.Result.Fail(e.Message);
            }
            
        }
    }
}
