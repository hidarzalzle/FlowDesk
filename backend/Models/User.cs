using Microsoft.AspNetCore.Identity;

namespace FlowDesk.Api.Models;

public class User : IdentityUser<int>
{
    public string FullName { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
