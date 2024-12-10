using social_media_app.Models;

namespace social_media_app.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(User user);
    }
}
