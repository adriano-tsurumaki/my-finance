using MediatR;
using api.Application.Auth.Dto;

namespace api.Application.Auth.Commands;

public record LoginUserCommand(string Email, string Password) : IRequest<LoginResponseDto>;
