
namespace WebApi.Modules.Orders;

public class GetOrdersByCostomerIDResponse
{

    public Guid Id { get; set; }

}
public class GetOrdersResponse
{

    public Guid CustomersID { get; set; }

}
public class InsertOrdersRequest
{
    public string OrdersList { get; set; } = null!;
    public Guid CustomersID { get; set; }
}

public class UpdateOrdersRequest
{
    
    public string OrdersList { get; set; } = null!;
}




