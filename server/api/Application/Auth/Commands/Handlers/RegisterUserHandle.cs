using MediatR;
using api.Domain.Entities;
using api.Infrastructure.Interfaces;

namespace api.Application.Auth.Commands.Handlers;

public class RegisterUserHandle(IUserRepository userRepository, IPasswordHasher passwordHasher) : IRequestHandler<RegisterUserCommand, bool>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<bool> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordHasher.HashPassword(command.User.Password);

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Name = command.User.Name,
            Email = command.User.Email,
            Password = hashedPassword,
        };

        await _userRepository.AddAsync(user);
        return true;
    }
}
