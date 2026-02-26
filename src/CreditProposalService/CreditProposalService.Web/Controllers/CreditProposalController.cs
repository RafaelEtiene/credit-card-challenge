using CreditProposalService.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditProposalService.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CreditProposalController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public CreditProposalController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProposals()
    {
        var result = await _mediator.Send(new GetCreditProposalQuery());
        
        if(!result.Any())
            return NotFound();
        
        return Ok(result);
    }
}