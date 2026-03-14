using FlowDesk.Api.DTOs;

namespace FlowDesk.Api.Services;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
}
