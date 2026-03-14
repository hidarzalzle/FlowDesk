using FlowDesk.Api.DTOs;
using FlowDesk.Api.Models;
using FlowDesk.Api.Repositories;

namespace FlowDesk.Api.Services;

public class ContentService : IContentService
{
    private readonly IContentRepository _repository;

    public ContentService(IContentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ContentItemDto>> GetAllAsync(bool isAdmin)
    {
        var items = await _repository.GetAllAsync(includeDrafts: isAdmin);
        return items.Select(Map).ToList();
    }

    public async Task<ContentItemDto?> GetByIdAsync(int id, bool isAdmin)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item is null || (!isAdmin && !item.IsPublished))
        {
            return null;
        }

        return Map(item);
    }

    public async Task<ContentItemDto> CreateAsync(UpsertContentItemDto request)
    {
        var entity = new ContentItem
        {
            Title = request.Title,
            Description = request.Description,
            IsPublished = request.IsPublished,
            CreatedAtUtc = DateTime.UtcNow
        };

        var added = await _repository.AddAsync(entity);
        return Map(added);
    }

    public async Task<ContentItemDto?> UpdateAsync(int id, UpsertContentItemDto request)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
        {
            return null;
        }

        existing.Title = request.Title;
        existing.Description = request.Description;
        existing.IsPublished = request.IsPublished;
        existing.UpdatedAtUtc = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(existing);
        return Map(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing);
        return true;
    }

    private static ContentItemDto Map(ContentItem item) => new()
    {
        Id = item.Id,
        Title = item.Title,
        Description = item.Description,
        IsPublished = item.IsPublished,
        CreatedAtUtc = item.CreatedAtUtc
    };
}
