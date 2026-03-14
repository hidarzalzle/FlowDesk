using FlowDesk.Api.Models;

namespace FlowDesk.Api.Repositories;

public interface IContentRepository
{
    Task<List<ContentItem>> GetAllAsync(bool includeDrafts);
    Task<ContentItem?> GetByIdAsync(int id);
    Task<ContentItem> AddAsync(ContentItem item);
    Task<ContentItem> UpdateAsync(ContentItem item);
    Task DeleteAsync(ContentItem item);
}
