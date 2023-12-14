using AutoMapper;

namespace WebApi.Modules.Orders;

public class OrdersMapper : Profile
{
    public OrdersMapper()
    {
        CreateMap<Orders, GetOrdersResponse>();
        CreateMap<Orders, GetOrdersByCostomerIDResponse>();
        CreateMap<InsertOrdersRequest, Orders>();
        
        CreateMap<UpdateOrdersRequest, Orders>();
    }
}
