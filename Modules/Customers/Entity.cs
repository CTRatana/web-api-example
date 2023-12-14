using WebApi.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace WebApi.Modules.Customers;

public class Customers : Entity
{
    public Guid CustomersID { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public ICollection<Orders.Orders> Orders { get; set; } = null!;
}


public class CustomersConfig : IEntityTypeConfiguration<Customers>
{
    public void Configure(EntityTypeBuilder<Customers> builder)
    {
    }
}