using AutoMapper;
using Account.Application.Features.Accounts.Queries;
using Account.Domain.Entities;

namespace Account.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Order, OrdersVm>().ReverseMap();
            //CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            //CreateMap<Order, UpdateOrderCommand>().ReverseMap();

            CreateMap<User, AccountByEmailRequest>().ReverseMap();

        }
    }
}
