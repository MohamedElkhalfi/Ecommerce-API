using Ecommerce.Core.Interfaces;
using Ecommerce.DataAccess.ConnexionDB;
using Ecommerce.DataAccess.Dto;
using Ecommerce.DataAccess.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Ecommerce.Core.Model;

namespace Ecommerce.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;

        public UserRepository(EcommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserModel> GetUserByUsernameAsync(string username)
        {
            var userEntity = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            return _mapper.Map<UserModel>(userEntity); // Mapping Entity → Model
        }

        public async Task AddUserAsync(UserModel userModel)
        {
            var userEntity = _mapper.Map<User>(userModel); // Mapping Model → Entity
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }
    }
}
