using Fintorly.Application.Features.Commands.AdvertisementCommands;
using Fintorly.Application.Features.Queries.AdvertisementQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fintorly.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdvertisementController : Controller
{
    private IMediator _mediator;

    public AdvertisementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Add(CreateAdvertisementCommand request)
    {
        var result=await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        else
            return BadRequest(result);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> Add(UpdateAdvertisementCommand request)
    {
        var result=await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        else
            return BadRequest(result);
    }
    
    [HttpGet("getAll")]
    public async Task<IActionResult> Get(GetAllAdvertisementQuery request)
    {
        var result=await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        else
            return BadRequest(result);
    }
     
    [HttpGet("getById")]
    public async Task<IActionResult> GetByIdAsync(GetByIdAdvertisementQuery request)
    {
        var result=await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        else
            return BadRequest(result);
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Add(DeleteAdvertisementCommand request)
    {
        var result=await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        else
            return BadRequest(result);
    }
}