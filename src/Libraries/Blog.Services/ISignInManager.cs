using Blog.Core.Domain;

namespace Blog.Services
{
    public interface ISignInManager
    {
        bool SignIn(string userName, string password);
        User CreatePassword(User user, string password);
    }
}
