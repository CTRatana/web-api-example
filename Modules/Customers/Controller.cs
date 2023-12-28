using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core;
using WebApi.Modules.Comstomers;


namespace WebApi.Modules.Customers;

public class CustomersController : MyController
{
    // private readonly ICloudStorageSingletonService _service;
    private readonly IMapper _mapper;
    private readonly ICustomersRepository _repository;

    public CustomersController(

        ICustomersRepository repository,
        IMapper mapper
        )
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllCustomers()
    {
        var customer = _repository.GetAll();
        var CustomersModel = _mapper.ProjectTo<GetCustomersResponse>(customer);

        return Ok(CustomersModel);
    }
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var customersId = _repository.GetSingle(e => e.Id == id && e.DeletedAt == null);
        var customersIdModel = _mapper.Map<GetCustomersResponse>(customersId);
        if (customersId == null)
        {
            return NotFound("Not Found");
        }
        return Ok(customersIdModel);
    }
    [HttpPost]
    public IActionResult CreateCustomers([FromBody] InsertCustomersRequest item)
    {
        if (item == null)
        {
            return BadRequest("invalid");
        }
        var createdCustomers = _mapper.Map<Customers>(item);
        _repository.Add(createdCustomers);
        _repository.Commit();

        return Ok();
    }
    [HttpPut("{id:guid}")]
    public IActionResult UpdateCustomers([FromBody] UpdateCustomersRequest request, Guid id)
    {
        var item = _repository.GetSingle(e => e.Id == id);
        if (item == null)
        {
            return NotFound("Not found");
        }
        if (request.FirstName != "")
        {
            request.FirstName = item.FirstName;
        }
        if (request.LastName != "")
        {
            request.LastName = item.LastName;
        }
        _mapper.Map(request, item);
        _repository.Update(item);
        _repository.Commit();
        return Ok();
    }
    [HttpDelete("{id:guid}")]

    public IActionResult DeleteById(Guid id)
    {
        var existed = _repository.Existed(e => e.Id == id);

        if (!existed)
        {
            return NotFound("Not Found");
        }
        var item = _repository.GetSingle(e => e.Id == id);
        item!.DeletedAt = DateTime.UtcNow;
        _repository.Update(item!);
        _repository.Commit();
        return Ok();
    }


}

