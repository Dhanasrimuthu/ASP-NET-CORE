using Microsoft.AspNetCore.Identity;
using storedetail.Model.Domain;

namespace storedetail.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(ApplicationUser user, List<string> role);
    }
}
