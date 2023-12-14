using WebApi.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Modules.Orders;

public class Orders : Entity
{
    public string OrdersList { get; set; } = null!;

    public Guid CustomersID { get; set; }
    public Customers.Customers Customers { get; set; } = null!;

}

public class OrdersConfig : IEntityTypeConfiguration<Orders>
{
    public void Configure(EntityTypeBuilder<Orders> builder)
    {
        builder.HasOne(o => o.Customers)
        .WithMany(s => s.Orders)
        .HasForeignKey(o => o.CustomersID);
    }
}