using Microsoft.EntityFrameworkCore;
using PosEnterprise.Core.Entities;
using PosEnterprise.Core.Interfaces;
using PosEnterprise.Infrastructure.Data;

namespace PosEnterprise.Infrastructure.Repositories;

public class BlogRepository : IBlogRepository
{
    private readonly ApplicationDbContext _context;

    public BlogRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BlogPost>> GetPublishedPostsAsync()
    {
        return await _context.BlogPosts
            .AsNoTracking()
            .Where(b => b.IsPublished)
            .OrderByDescending(b => b.PublishedAt)
            .ToListAsync();
    }

    public async Task<BlogPost?> GetBySlugAsync(string slug)
    {
        return await _context.BlogPosts
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Slug.ToLower() == slug.ToLower() && b.IsPublished);
    }

    public async Task<BlogPost?> GetByIdAsync(int id)
    {
        return await _context.BlogPosts.FindAsync(id);
    }

    public async Task<IEnumerable<BlogPost>> GetAllPostsAsync()
    {
        return await _context.BlogPosts
            .AsNoTracking()
            .OrderByDescending(b => b.PublishedAt)
            .ToListAsync();
    }

    public async Task AddAsync(BlogPost post)
    {
        await _context.BlogPosts.AddAsync(post);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(BlogPost post)
    {
        _context.BlogPosts.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var post = await _context.BlogPosts.FindAsync(id);
        if (post != null)
        {
            _context.BlogPosts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}