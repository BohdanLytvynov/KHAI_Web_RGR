using BookStore.BLL.Dto.JWTToken;
using BookStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Services.TokenServices.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponseDTO> GenerateAccesToken(User user, Action<List<Claim>> claimMod = null!);

        string? GetUserClaimFromAccessToken(string accessToken, string claimName);
    }
}
