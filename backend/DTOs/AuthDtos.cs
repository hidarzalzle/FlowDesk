namespace FlowDesk.Api.DTOs;

public record LoginRequestDto(string Email, string Password);

public record AuthResponseDto(
    string Token,
    DateTime ExpiresAtUtc,
    string Email,
    string FullName,
    IList<string> Roles);
