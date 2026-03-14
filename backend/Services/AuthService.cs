using FlowDesk.Api.Authentication;
using FlowDesk.Api.DTOs;
using FlowDesk.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace FlowDesk.Api.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public AuthService(UserManager<User> userManager, IJwtTokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return null;
        }

        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isValidPassword)
        {
            return null;
        }

        var roles = await _userManager.GetRolesAsync(user);
        var (token, expiresAtUtc) = _tokenGenerator.Generate(user, roles);

        return new AuthResponseDto(token, expiresAtUtc, user.Email ?? string.Empty, user.FullName, roles);
    }
}
