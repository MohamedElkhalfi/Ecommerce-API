using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> Authenticate(string username, string password);
        Task<bool> Register(string username, string password, string role);
    }
}
