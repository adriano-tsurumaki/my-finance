using MediatR;
using Microsoft.AspNetCore.Mvc;
using api.Application.Auth.Commands;
using api.Application.Auth.Dto;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var command = new RegisterUserCommand(dto);

            var result = await _mediator.Send(command);

            return result ? Ok("User created succesfully") : BadRequest("Failed to create user");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
