using FlowDesk.Api.DTOs;

namespace FlowDesk.Api.Services;

public interface IUserService
{
    Task<ProfileDto?> GetProfileAsync(int userId);
    Task<List<UserSummaryDto>> GetUsersAsync();
}
