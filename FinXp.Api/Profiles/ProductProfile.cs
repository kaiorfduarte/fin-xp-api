using AutoMapper;
using FinXp.Domain.Model;
using FinXp.Domain.Model.Response;

namespace FinXp.Api.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.ProductId, src => src.MapFrom(x => x.ProductId))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Quantity, src => src.MapFrom(x => x.Quantity))
                .ForMember(dest => dest.RegisterDate, src => src.MapFrom(x => x.RegisterDate))
                .ForMember(dest => dest.ProductDueDate, src => src.MapFrom(x => x.ProductDueDate));

        CreateMap<ClientProduct, ClientProductResponse>()
                    .ForMember(dest => dest.ProductId, src => src.MapFrom(x => x.ProductId))
                    .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                    .ForMember(dest => dest.Quantity, src => src.MapFrom(x => x.Quantity))
                    .ForMember(dest => dest.RegisterDate, src => src.MapFrom(x => x.RegisterDate));
    }
}
