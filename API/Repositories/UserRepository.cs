using API.Data;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly APIEntities _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuaration;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, APIEntities context, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _mapper = mapper;
            _configuaration = configuration;
        }
        public async Task<TokenModel?> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };


                var token = GenerateToken(authClaims);

                var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
                var refreshToken = GeneraRefreshToken();

                var a = new Token()
                {
                    token = refreshToken,
                    JtiID = token.Id,
                    IsUsed = false,
                    IsRevoked = false,
                    UserID = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    ExpiredAt = DateTime.UtcNow.AddMinutes(30)
                };

                _context.Tokens.Add(a);
                await _context.SaveChangesAsync();

                return new TokenModel()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                };

            }

            return null;

        }

        public async Task<IdentityResult> Register(RegisterModel model)
        {
            var user = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuaration["JWT:Secret"]));

            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        private JwtSecurityToken GenerateToken(List<Claim> claims)
        {
            SigningCredentials signingCredentials = GetSigningCredentials();

            var token = new JwtSecurityToken(
                issuer: _configuaration["JWT:Issuer"],
                audience: _configuaration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signingCredentials
                );

            return token;
        }

        private string GeneraRefreshToken()
        {
            var randomNumber = new Byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        public Task<TokenModel> RenewToken(TokenModel model)
        {
        }
    }
}
