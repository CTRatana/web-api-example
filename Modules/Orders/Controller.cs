using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core;
using WebApi.Modules.Orders;


public class OrdersController : MyController
{
    // private readonly ICloudStorageSingletonService _service;
    private readonly IMapper _mapper;
    private readonly IOrdersRepository _repository;

    public OrdersController(
        IOrdersRepository repository,
        IMapper mapper
        )
    {
        _repository = repository;
        _mapper = mapper;
    }
    [HttpGet]
    public IActionResult GetAllOrders()
    {
        var customer = _repository.GetAll();
        var OrdersModel = _mapper.ProjectTo<GetOrdersResponse>(customer);

        return Ok(OrdersModel);
    }
    // [HttpGet("{id:guid}")]
    // public IActionResult GetById(Guid id)
    // {
    //     var OrdersId = _repository.GetSingle(e => e.Id == id);

    //     var OrdersIdModel = _mapper.Map<GetOrdersResponse>(OrdersId);
    //     if (OrdersId == null)
    //     {
    //         return NotFound("Not Found");
    //     }
    //     return Ok(OrdersIdModel);
    // }
    [HttpGet("{id:guid}")]
    public IActionResult GetOrdersByCustomerId(Guid id)
    {
        var OrdersId = _repository.FindBy(e => e.CustomersID == id);
        if (OrdersId == null)
        {

            return NotFound("Not Found");
        }

        var OrdersIdModel = _mapper.ProjectTo<GetOrdersByCostomerIDResponse>(OrdersId);

        return Ok(OrdersIdModel);
    }
    [HttpPost]
    public IActionResult CreateProfile([FromBody] InsertOrdersRequest item)
    {
        if (item == null)
        {
            return BadRequest("invalid");
        }
        var createdOrders = _mapper.Map<Orders>(item);
        _repository.Add(createdOrders);
        _repository.Commit();

        return Ok();
    }
    [HttpPut("{id:guid}")]
    public IActionResult UpdateOrders([FromBody] UpdateOrdersRequest request, Guid id)
    {
        var item = _repository.GetSingle(e => e.Id == id);
        if (item == null)
        {
            return NotFound("Not found");
        }
        if (request.OrdersList != "")
        {
            request.OrdersList = item.OrdersList;
        }
        _mapper.Map(request, item);
        _repository.Update(item);
        _repository.Commit();
        return Ok();
    }
    [HttpDelete("{id:guid}")]

    public IActionResult DeleteById(Guid id)
    {
        var profileId = _repository.GetSingle(e => e.Id == id);

        if (profileId == null)
        {
            return NotFound("Not Found");
        }
        _repository.Remove(profileId);
        _repository.Commit();
        return Ok();
    }
}

