using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
using WebApi.Core;
using WebApi.Modules.Comstomers;


namespace WebApi.Modules.Customers;

public class CustomersController : MyController
{
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
    public IActionResult Gets([FromQuery] PaginationRequest item)
    {
        var iQueryable = _repository.FindBy(e => e.DeletedAt == null); //find not deleted id of cumstomers  
        //check for sort by firstname
        if (item.Sort == "fn")
        {
            iQueryable = iQueryable.OrderBy(e => e.FirstName);
        }
        //check for sort by lastname
        else if (item.Sort == "ln")
        {
            iQueryable = iQueryable.OrderBy(e => e.LastName);
        }
        //check for sort by no order
        else
        {
            iQueryable = iQueryable.OrderBy(e => e.DeletedAt);
        }
        var afterfiler = iQueryable //filter the deleted id
            .Skip((item.CurrentPage - 1) * item.PageSize) //sort item by 10 of one page 
            .Take(item.PageSize); //get not deleted item that put in pagesize
        var results = _mapper.ProjectTo<GetCustomersResponse>(afterfiler).ToList();//then filter item to list
        return Ok(results);//return list of result
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var customersId = _repository.GetSingle(e => e.Id == id && e.DeletedAt == null);//go to database to find specifix id 
        var customersIdModel = _mapper.Map<GetCustomersResponse>(customersId);//go to overall model model in db and point to only id model 
        if (customersId == null)//check id in null or not 
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
        var createdCustomers = _mapper.Map<Customers>(item);//map for model to push information in correct column
        _repository.Add(createdCustomers);//add the detail column of customer 
        _repository.Commit();//add cumstomer information to db

        return Ok();
    }
    [HttpPut("{id:guid}")]
    public IActionResult UpdateCustomers([FromBody] UpdateCustomersRequest request, Guid id)
    {
        var item = _repository.GetSingle(e => e.Id == id);//go to db and find the id of customer
        if (item == null)
        {
            return NotFound("Not found");
        }
        //check Firstname and Lastname if not empty or empty
        if (request.FirstName != "")
        {
            request.FirstName = item.FirstName;
        }
        if (request.LastName != "")
        {
            request.LastName = item.LastName;
        }
        _mapper.Map(request, item);//map firstname or lastname in change
        _repository.Update(item);//add new change lastname or firstname t
        _repository.Commit();//push new change to db
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteById(Guid id)
    {
        var existed = _repository.Existed(e => e.Id == id);//check out id in db
        //find id exite or not
        if (!existed)
        {
            return NotFound("Not Found");
        }
        //if exit go to db and get id 
        var item = _repository.GetSingle(e => e.Id == id);
        //update db in deleteat column
        item!.DeletedAt = DateTime.UtcNow;
        //pust data deletedat to db
        _repository.Update(item!);
        _repository.Commit();
        return Ok();
    }


}

