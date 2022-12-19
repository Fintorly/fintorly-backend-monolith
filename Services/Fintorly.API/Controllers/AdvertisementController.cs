using Fintorly.Application.Features.Commands.AdvertisementCommands;
using Fintorly.Application.Features.Queries.AdvertisementQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fintorly.API.Controllers;

public class AdvertisementController : Controller
{
    private IMediator _mediator;

    public AdvertisementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("addAdvertisement")]
    public async Task<IActionResult> Add(CreateAdvertisementCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
    
    [HttpGet("get")]
    public async Task<IActionResult> Get(GetAllAdvertisementQuery request)
    {
        return Ok(await _mediator.Send(request));
    }
}