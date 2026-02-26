using CardService.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CardService.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CardController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public CardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCards()
    {
        var result = await _mediator.Send(new GetCardQuery());
        
        if(!result.Any())
            return NotFound();
        
        return Ok(result);
    }
}