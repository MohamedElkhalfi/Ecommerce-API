using System.Threading.Tasks;
using Ecommerce.Core.Model;

namespace Ecommerce.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByUsernameAsync(string username);
        Task AddUserAsync(UserModel userModel);
    }
}
