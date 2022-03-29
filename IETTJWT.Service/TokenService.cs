using IETTJWT.Core;
using IETTJWT.Core.Models;
using IETTJWT.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IETTJWT.Service
{
    public class TokenService
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration, AppDbContext context)
        {

            _configuration = configuration;
            _context = context;
        }

        public async Task<TokenDto> CreateToken(AppUser user, List<string> roles = null)
        {




            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenOptions:SecurityKey"]));


            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);


            var claims = new List<Claim>()
            {

                new(ClaimTypes.NameIdentifier,user.Id),
                new(ClaimTypes.Email,user.Email),
                new (ClaimTypes.Name,user.UserName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Aud,"www.miniapp1.com"),
                new(JwtRegisteredClaimNames.Aud,"www.miniapp2.com"),
                new("city",user.City),
                new("birthDay",new DateTime(2010,5,20).ToLongDateString())

            };

            if (roles != null && roles.Any())
            {
                roles.ForEach(x =>
                {
                    claims.Add(new(ClaimTypes.Role, x));

                });




            }



            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: "https://localhost:7089",
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingCredentials,
                notBefore: DateTime.Now,
                claims: claims);


            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.WriteToken(jwtSecurityToken);



            var anyRefreshToken = _context.userRefreshTokens.FirstOrDefault(x => x.UserId == user.Id);

            var userRefreshToken = new UserRefreshToken()
            {
                UserId = user.Id,
                RefreshToken = Guid.NewGuid().ToString(),
                Expiration = DateTime.Now.AddDays(45)
            };

            if (anyRefreshToken != null)
            {
                anyRefreshToken.RefreshToken = userRefreshToken.RefreshToken;
                anyRefreshToken.Expiration = userRefreshToken.Expiration;
            }
            else
            {
                await _context.userRefreshTokens.AddAsync(userRefreshToken);
            }





            await _context.SaveChangesAsync();

            return new TokenDto()
            {

                AccessToken = token,
                RefreshToken = userRefreshToken.RefreshToken,
                RefreshTokenExpiration = userRefreshToken.Expiration,

            };

        }





        // UserManager / RoleManager /SigninManager
    }
}
