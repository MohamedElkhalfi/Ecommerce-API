using AutoMapper;
using Ecommerce.Api.Model;
using Ecommerce.Core.Model;
using Ecommerce.Core.Services;
 

namespace ErpMacz.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
          
            //Domain Ressource  => API Domain
            CreateMap<ProductModel, ProductApi>()
        .ForMember(des => des.id, opt => opt.MapFrom(src => src.ID))
        .ForMember(des => des.description, opt => opt.MapFrom(src => src.Description))
        .ForMember(des => des.currentPrice, opt => opt.MapFrom(src => src.CurrentPrice))
        .ForMember(des => des.available, opt => opt.MapFrom(src => src.Is_Available))
        .ForMember(des => des.promotion, opt => opt.MapFrom(src => src.Is_Promotion))
        .ForMember(des => des.selected, opt => opt.MapFrom(src => src.Is_Selected))
        .ForMember(des => des.name, opt => opt.MapFrom(src => src.Name))
        .ForMember(des => des.photoName, opt => opt.MapFrom(src => src.PhotoName))
        .ForMember(des => des.quantity, opt => opt.MapFrom(src => src.Quantity)) ;


        }
    }
}