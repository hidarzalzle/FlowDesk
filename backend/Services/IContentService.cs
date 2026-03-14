using FlowDesk.Api.DTOs;

namespace FlowDesk.Api.Services;

public interface IContentService
{
    Task<List<ContentItemDto>> GetAllAsync(bool isAdmin);
    Task<ContentItemDto?> GetByIdAsync(int id, bool isAdmin);
    Task<ContentItemDto> CreateAsync(UpsertContentItemDto request);
    Task<ContentItemDto?> UpdateAsync(int id, UpsertContentItemDto request);
    Task<bool> DeleteAsync(int id);
}
