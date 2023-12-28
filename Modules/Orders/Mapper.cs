using AutoMapper;

namespace WebApi.Modules.Orders;

public class OrdersMapper : Profile
{
    public OrdersMapper()
    {
        CreateMap<Orders, GetOrdersResponse>()

            .ForMember(m => m.FirstName, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Customers.FirstName))

            .ForMember(l => l.LastName, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Customers.LastName));

        CreateMap<Orders, GetOrdersByCostomerIDResponse>();
        CreateMap<InsertOrdersRequest, Orders>();

        CreateMap<UpdateOrdersRequest, Orders>();
    }
}
