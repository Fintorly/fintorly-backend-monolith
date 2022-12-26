using Fintorly.Application.Features.Commands.AnswerCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IResult = Fintorly.Domain.Common.IResult;

namespace Fintorly.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AnswerController : Controller
{
    private readonly IMediator _mediator;

    public AnswerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add")]
    [ProducesResponseType(typeof(IResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult), StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> AddAsync(CreateAnswerMultipleCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
}