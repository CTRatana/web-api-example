using AutoMapper;
using WebApi.Modules.Comstomers;

namespace WebApi.Modules.Customers;

public class CustomersMapper : Profile
{
    public CustomersMapper()
    {
        CreateMap<Customers, GetCustomersResponse>();
        CreateMap<InsertCustomersRequest, Customers>();
        CreateMap<UpdateCustomersRequest, Customers>();
    }
}
