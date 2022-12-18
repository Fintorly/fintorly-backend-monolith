using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fintorly.Application.Features.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fintorly.API.Controllers
{
    public class UserController : Controller
    {
        IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(CreateUserCommand createUser)
        {
            var result = await _mediator.Send(createUser);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }
    }
}

