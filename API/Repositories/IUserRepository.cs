using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Repositories
{
    public interface IUserRepository
    {
        public Task<TokenModel?> Login(LoginModel model);
        public Task<IdentityResult> Register(RegisterModel model);
        public Task<TokenModel> RenewToken(TokenModel model);
    }
}
