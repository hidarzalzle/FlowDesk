using FlowDesk.Api.DTOs;
using FlowDesk.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.Api.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ProfileDto?> GetProfileAsync(int userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null) return null;

        var roles = await _userManager.GetRolesAsync(user);
        return new ProfileDto
        {
            Email = user.Email ?? string.Empty,
            FullName = user.FullName,
            Roles = roles
        };
    }

    public async Task<List<UserSummaryDto>> GetUsersAsync()
    {
        var users = await _userManager.Users.OrderBy(u => u.Email).ToListAsync();
        var list = new List<UserSummaryDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            list.Add(new UserSummaryDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                FullName = user.FullName,
                Roles = roles
            });
        }

        return list;
    }
}
