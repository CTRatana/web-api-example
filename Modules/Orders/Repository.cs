using WebApi.Core;

namespace WebApi.Modules.Orders;

public interface IOrdersRepository : IRepository<Orders>
{

}

public class OrdersRepository : Repository<Orders>, IOrdersRepository
{
    public OrdersRepository(MyDbContext context) : base(context)
    {
    }

}
