using BookshopApi.Models;

namespace BookshopApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
