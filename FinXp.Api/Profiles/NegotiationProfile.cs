using AutoMapper;
using FinXp.Domain.Model;
using FinXp.Domain.Model.Request;

namespace FinXp.Api.Profiles
{
    public class NegotiationProfile : Profile
    {
        public NegotiationProfile()
        {
            CreateMap<NegotiationRequest, NegotiationProduct>()
                .ForMember(dest => dest.ProductId, src => src.MapFrom(x => x.ProductId))
                .ForMember(dest => dest.ClientId, src => src.MapFrom(x => x.ClientId))
                .ForMember(dest => dest.Quantity, src => src.MapFrom(x => x.Quantity))
                .ForMember(dest => dest.OperationTypeId, src => src.MapFrom(x => x.OperationTypeId));
        }
    }
}
