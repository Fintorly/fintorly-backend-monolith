using System.Net;
using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Application.Features.Commands.AuthCommands;
using Fintorly.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IResult = Fintorly.Domain.Common.IResult;

namespace Fintorly.API.Controllers;

public class AuthController : Controller
{
    private IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("loginWithMail")]
    [ProducesResponseType(typeof(UserAndTokenDto),StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginWithMail(LoginWithMailCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
    [ProducesResponseType(typeof(IResult<UserAndTokenDto>),StatusCodes.Status200OK)]
    [HttpPost("loginWithPhone")]
    public async Task<IActionResult> LoginWithPhone(LoginWithPhoneCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPost("loginWithUserName")]
    [ProducesResponseType(typeof(UserAndTokenDto),StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginWithMail(LoginWithUserNameCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
    
    [HttpPost("register")]
    [ProducesResponseType(typeof(UserAndTokenDto),StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(RegisterCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
 
    [HttpPost("verificationCodeAddAsync")]
    public async Task<IActionResult> verificationCodeAddAsync(VerificationCodeAddCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("sendActivationCodeEmailAddress")]
    public async Task<IActionResult> sendActivationCodeEmailAddress(SendActivationCodeEmailAddressCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
}