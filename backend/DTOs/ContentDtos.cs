namespace FlowDesk.Api.DTOs;

public class ContentItemDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}

public class UpsertContentItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsPublished { get; set; } = true;
}
