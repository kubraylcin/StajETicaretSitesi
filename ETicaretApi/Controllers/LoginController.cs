using ETicaret.DtoLayer.LoginDto;
using ETicaret.DtoLayer.Settings;
using ETicaretEntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ETicaretApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.Username);
            if (user == null)
                return Unauthorized("Kullanıcı adı veya şifre yanlış.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Kullanıcı adı veya şifre yanlış.");

            // Claims oluşturma
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id), // UserId'yi bu claim ile ekliyoruz
                new Claim(ClaimTypes.Name, user.UserName)
                // İsteğe bağlı olarak rol bilgilerini de ekleyebilirsiniz:
                // new Claim(ClaimTypes.Role, "Admin"),
            };

            // Token oluşturma
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_jwtSettings.ExpiresInDays);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expires,
                signingCredentials: credentials
            );
            
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { Token = jwtToken, Expiration = expires });
        }
    }
}