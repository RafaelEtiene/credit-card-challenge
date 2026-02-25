using CustomerService.Application.Commands;
using CustomerService.Web.Model.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
    {
        if(request is null)
            return BadRequest();
        
        await _mediator.Send(new CreateCustomerCommand(request.Name, request.DocumentNumber, request.BirthDate, request.Score));

        return Created();
    }
}