
namespace WebApi.Modules.Comstomers;

public class GetCustomersResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}

public class InsertCustomersRequest
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}

public class UpdateCustomersRequest
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
}
