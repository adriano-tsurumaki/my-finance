using MediatR;
using server.Application.Auth.Dto;

namespace server.Application.Auth.Commands;

public record RegisterUserCommand(RegisterUserDto User) : IRequest<bool>;
