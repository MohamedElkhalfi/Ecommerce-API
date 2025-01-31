using AutoMapper;
using Ecommerce.DataAccess.Dto;
using Ecommerce.DataAccess.Model;
using Ecommerce.Core.Model;

namespace Ecommerce.DataAccess.Dto.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // Mapping entre DataAccess.Model.User et DataAccess.Dto.UserDto
            CreateMap<User, UserDto>().ReverseMap();

            // Mapping entre DataAccess.Dto.UserDto et Core.Model.UserModel
            CreateMap<UserDto, UserModel>().ReverseMap();
        }
    }
}
