using IETTJWT.Core;
using IETTJWT.Core.DTOs;
using IETTJWT.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IETTJWT.Service
{
    public class AuthService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        private readonly TokenService _tokenService;
        public AuthService(UserManager<AppUser> userManager, TokenService tokenService, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }

        public async Task<ResponseDto<TokenDto>> CreateTokenAsync(LoginDto login)
        {






            if (login == null) throw new ArgumentNullException(nameof(login));

            var hasUser = await _userManager.FindByEmailAsync(login.Email);

            if (hasUser == null) return ResponseDto<TokenDto>.Fail("Email veya şifre yanlış", 400);

            if (!await _userManager.CheckPasswordAsync(hasUser, login.Password))
            {
                if (hasUser == null) return ResponseDto<TokenDto>.Fail("Email veya şifre yanlış", 400);
            }

            //Claim ekleme
            //await _userManager.AddClaimAsync(hasUser, new Claim("AKey", "AValue"));
            //await _userManager.AddClaimAsync(hasUser, new Claim("BKey", "BValue"));



            //Örnek rol ekleme
            //await _roleManager.CreateAsync(new AppRole() { Name = "admin" });
            //await _roleManager.CreateAsync(new AppRole() { Name = "manager" });


            //await _userManager.AddToRoleAsync(hasUser, "admin");

            //await _userManager.AddToRoleAsync(hasUser, "manager");

            var roles = await _userManager.GetRolesAsync(hasUser);

            var claims = _userManager.GetClaimsAsync(hasUser);



            var token = await _tokenService.CreateToken(hasUser, roles.ToList());


            return ResponseDto<TokenDto>.Success(token, 200);

        }


        public async Task<ResponseDto<bool>> CreateUser(CreateUserDto user)
        {
            var result = await _userManager.CreateAsync(new AppUser() { UserName = user.UserName, Email = user.Email, City = "İstanbul" }, user.Password);



            if (result.Succeeded)
            {
                return ResponseDto<bool>.Success(true, 200);
            }
            else
            {
                return ResponseDto<bool>.Fail(result.Errors.Select(x => x.Description).ToList(), 400);
            }
        }


    }
}
