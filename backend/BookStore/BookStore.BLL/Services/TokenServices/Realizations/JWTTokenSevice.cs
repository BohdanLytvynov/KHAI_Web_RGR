using AutoMapper;
using BookStore.BLL.Dto.JWTToken;
using BookStore.BLL.Services.TokenServices.Interfaces;
using BookStore.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Services.TokenServices.Realizations
{
    public class JWTTokenSevice : ITokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly JWTTokenConfiguration _tokensConfiguration;
        private readonly IMapper _mapper;

        public JWTTokenSevice(UserManager<User> userManager,
            JWTTokenConfiguration tokensConfiguration,
            IMapper mapper)
        {
            _userManager = userManager;
            _tokensConfiguration = tokensConfiguration;
            _mapper = mapper;
        }

        private string GenerateAccessToken(User user, List<Claim> claims)
        {
            if (!claims.Any())
            {
                throw new ArgumentNullException("Claims not exists!");
            }

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_tokensConfiguration.SecretKey!));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            var descriptor = new SecurityTokenDescriptor()
            {
                Issuer = _tokensConfiguration.Issuer,
                Audience = _tokensConfiguration.Audience,
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials,                
            };
            return new JwtSecurityTokenHandler().CreateEncodedJwt(descriptor);
        }

        private async Task<List<Claim>> GetUserClaimsAsync(User user, Action<List<Claim>> claimMod)
        {
            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Any())
            {
                throw new ArgumentNullException("Roles for User not found!");
            }
            
            List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),//User Id                                                                                                                                         
            new Claim(ClaimTypes.Role, roles.First())//Role
        };

            if (claimMod is not null)
                claimMod(claims);

            return claims;
        }

        public string? GetUserClaimFromAccessToken(string accessToken, string claimName)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(null, "Invalid Token");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(accessToken) as JwtSecurityToken;
            var userIdClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type.Equals(claimName))?.Value;
            return userIdClaim;
        }

        public async Task<TokenResponseDTO> GenerateAccesToken(User user, Action<List<Claim>> claimMod)
        {
            if (user is null)
            {
                throw new ArgumentNullException(null, "UserNotFound");
            }

            var tokenResponse = new TokenResponseDTO();
            var userClaims = await GetUserClaimsAsync(user, claimMod);
            tokenResponse.AccessToken = GenerateAccessToken(user, userClaims);

            return tokenResponse;
        }
    }
}
