using Fintorly.Application.Features.Commands.AuthCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fintorly.API.Controllers;

public class AuthController : Controller
{
    private IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("verificationCodeAddAsync")]
    public async Task<IActionResult> Generate(VerificationCodeAddCommand request)
    {
      return Ok(await _mediator.Send(request));
    }
    
    [HttpPost("sendActivationCodeEmailAddress")]
    public async Task<IActionResult> Generate(SendActivationCodeEmailAddressCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
}