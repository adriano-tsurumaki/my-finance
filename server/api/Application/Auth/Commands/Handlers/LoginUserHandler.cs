using MediatR;
using api.Application.Auth.Dto;
using api.Domain.Enums;
using api.Infrastructure.Interfaces;
using api.Infrastructure.Security;

namespace api.Application.Auth.Commands.Handlers;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null || !_passwordHasher.VerifyPassword(user.Password, request.Password))
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new LoginResponseDto
        {
            Token = token,
            Email = user.Email,
            Name = user.Name,
            Role = user.Role.GetDescription()
        };
    }
}
