using FlowDesk.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.Api.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<ContentItem> ContentItems => Set<ContentItem>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ContentItem>(entity =>
        {
            entity.Property(x => x.Title).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(1000);
        });
    }
}
