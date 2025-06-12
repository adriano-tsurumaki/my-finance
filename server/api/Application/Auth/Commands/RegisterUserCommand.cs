using MediatR;
using api.Application.Auth.Dto;

namespace api.Application.Auth.Commands;

public record RegisterUserCommand(RegisterUserDto User) : IRequest<bool>;
