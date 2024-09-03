using eAppointmentServer.Application.Services;
using eAppointmentServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Auth.Login;

internal sealed class LoginCommandHandler(UserManager<AppUser> userManager, IJwtProvider jwtProvider) 
    : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        AppUser? appUser = await userManager.Users
            .FirstOrDefaultAsync(p =>
                p.Email == request.UsernameOrEmail ||
                p.UserName == request.UsernameOrEmail, cancellationToken);

        if (appUser is null)
        {
            return Result<LoginCommandResponse>.Failure("User not found");
        }

        bool isPasswordCorrrect = await userManager.CheckPasswordAsync(appUser, request.Password);
        if (!isPasswordCorrrect)
        {
            return Result<LoginCommandResponse>.Failure("Password is wrong");
        }
        return Result<LoginCommandResponse>.Succeed(new (jwtProvider.CreateToken(appUser)));
    }
}