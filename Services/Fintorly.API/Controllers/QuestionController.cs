using Fintorly.Application.Features.Commands.QuestionCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IResult = Fintorly.Domain.Common.IResult;

namespace Fintorly.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionController : Controller
{
   private readonly IMediator _mediator;

   public QuestionController(IMediator mediator)
   {
      _mediator = mediator;
   }

   [HttpPost("add")]
   [ProducesResponseType(typeof(IResult), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(IResult), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> AddAsync(CreateQuestionCommand command)
   {
      var result = await _mediator.Send(command);
      if (result.Succeeded)
         return Ok(result);
      return BadRequest(result);
   }
   
}