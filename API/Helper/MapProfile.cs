using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

namespace API.Helper
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<Games, GamesToReturnDto>()
                .ForMember(b =>b.ConsoleDevice, x => x.MapFrom(c => c.ConsoleDevice.Name))
                .ForMember(b => b.Genre, x => x.MapFrom(c => c.Genre.Name))
                .ForMember(b => b.PictureUrl, x=> x.MapFrom<GameUrlResolver>());

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<ShoppingCartDto, ShoppingCart>();

            CreateMap<ShoppingCartItemDto, ShoppingCartItem>();

            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(x => x.DeliveryMethods, o => o.MapFrom(b => b.DeliveryMethods.ShortName))
                .ForMember(x => x.ShippingPrice, o => o.MapFrom(b => b.DeliveryMethods.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(x => x.ProductId, o => o.MapFrom(b => b.ItemOrdered.ProductItemId))
                .ForMember(x => x.ProductName, o => o.MapFrom(b => b.ItemOrdered.ProductName))
                .ForMember(x => x.PictureUrl, o => o.MapFrom(b => b.ItemOrdered.PictureUrl))
                .ForMember(x => x.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());
        }
    }
}
