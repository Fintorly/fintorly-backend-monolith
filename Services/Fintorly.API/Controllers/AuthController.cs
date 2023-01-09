using System.Net;
using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Application.Features.Commands.AuthCommands;
using Fintorly.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IResult = Fintorly.Domain.Common.IResult;

namespace Fintorly.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("loginWithMail")]
    [ProducesResponseType(typeof(IResult<UserAndTokenDto>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginWithMail(LoginWithMailCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("loginWithPhone")]
    [ProducesResponseType(typeof(IResult<UserAndTokenDto>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginWithPhone(LoginWithPhoneCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPost("loginWithUserName")]
    [ProducesResponseType(typeof(UserAndTokenDto),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginWithMail(LoginWithUserNameCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
    
    [HttpPost("register")]
    [ProducesResponseType(typeof(UserAndTokenDto),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
    
    [HttpPost("changePassword")]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(ChangePasswordCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
    
    [HttpPost("emailActive")]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(EmailActiveCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
    
    [HttpPost("phoneActive")]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(PhoneActiveCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
    
    [HttpPost("forgotPasswordEmail")]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(ForgotPasswordEmailCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
    
    [HttpPost("forgotPasswordPhone")]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(ForgotPasswordPhoneCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
    
    [HttpPost("sendActivationCodeEmailAddress")]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(SendActivationCodeEmailAddressCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
    
    [HttpPost("sendActivationCodePhoneNumber")]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(SendActivationCodePhoneNumberCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
 
    [HttpPost("verificationCodeAdd")]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResult),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerificationCodeAddAsync(VerificationCodeAddCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.Succeeded)
            return Ok(result);
        return BadRequest(result);
    }
}