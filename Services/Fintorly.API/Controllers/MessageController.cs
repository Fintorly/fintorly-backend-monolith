using Fintorly.Application.Dtos.MessageDtos;
using Fintorly.Application.Features.Commands.AdvertisementCommands;
using Fintorly.Application.Features.Commands.MessageCommands;
using Fintorly.Application.Features.Queries.AdvertisementQueries;
using Fintorly.Application.Features.Queries.MessageQueries;
using Fintorly.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IResult = Fintorly.Domain.Common.IResult;

namespace Fintorly.API.Controllers;

public class MessageController : Controller
{
    private IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ProducesResponseType(typeof(IResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult), StatusCodes.Status400BadRequest)]
    [HttpPost("create")]
    public async Task<IActionResult> Add(CreateMessageCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }

    [ProducesResponseType(typeof(IResult<List<MessageDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult), StatusCodes.Status400BadRequest)]
    [HttpGet("getAllMessageByGroupIdQuery")]
    public async Task<IActionResult> Get(GetAllMessageByGroupIdQuery request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }

    [ProducesResponseType(typeof(IResult<List<MessageDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult), StatusCodes.Status400BadRequest)]
    [HttpGet("getAllMessageByMentorIdQuery")]
    public async Task<IActionResult> Get(GetAllMessageByMentorIdQuery request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }

    [ProducesResponseType(typeof(IResult<MessageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult), StatusCodes.Status400BadRequest)]
    [HttpGet("getById")]
    public async Task<IActionResult> GetByIdAsync(GetMessageQuery request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
}