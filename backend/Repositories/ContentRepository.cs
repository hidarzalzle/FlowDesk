using FlowDesk.Api.Data;
using FlowDesk.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.Api.Repositories;

public class ContentRepository : IContentRepository
{
    private readonly AppDbContext _context;

    public ContentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ContentItem>> GetAllAsync(bool includeDrafts)
    {
        var query = _context.ContentItems.AsQueryable();

        if (!includeDrafts)
        {
            query = query.Where(x => x.IsPublished);
        }

        return await query.OrderByDescending(x => x.CreatedAtUtc).ToListAsync();
    }

    public Task<ContentItem?> GetByIdAsync(int id) => _context.ContentItems.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<ContentItem> AddAsync(ContentItem item)
    {
        _context.ContentItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<ContentItem> UpdateAsync(ContentItem item)
    {
        _context.ContentItems.Update(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task DeleteAsync(ContentItem item)
    {
        _context.ContentItems.Remove(item);
        await _context.SaveChangesAsync();
    }
}
