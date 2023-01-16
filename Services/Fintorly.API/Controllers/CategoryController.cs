using Fintorly.Application.Features.Commands.CategoryCommands;
using Fintorly.Application.Features.Queries.CategoryQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fintorly.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : Controller
{
    private IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Add(CreateCategoryCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        else
            return BadRequest(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Add(UpdateCategoryCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        else
            return BadRequest(result);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> Get(GetAllCategoryQuery request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        else
            return BadRequest(result);
    }

    [HttpGet("getById")]
    public async Task<IActionResult> GetByIdAsync(GetByIdCategoryQuery request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        else
            return BadRequest(result);
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Add(DeleteCategoryCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        else
            return BadRequest(result);
    }
}