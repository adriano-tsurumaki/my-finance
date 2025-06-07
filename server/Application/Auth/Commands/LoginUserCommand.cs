using MediatR;
using server.Application.Auth.Dto;

namespace server.Application.Auth.Commands;

public record LoginUserCommand(string Email, string Password) : IRequest<LoginResponseDto>;
