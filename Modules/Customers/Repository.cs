using WebApi.Core;

namespace WebApi.Modules.Customers;

public interface ICustomersRepository : IRepository<Customers>
{

}

public class CustomersRepository : Repository<Customers>, ICustomersRepository
{
    public CustomersRepository(MyDbContext context) : base(context)
    {
    }

}
