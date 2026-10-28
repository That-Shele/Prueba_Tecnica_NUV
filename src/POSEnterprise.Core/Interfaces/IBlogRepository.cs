using PosEnterprise.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PosEnterprise.Core.Interfaces;

public interface IBlogRepository
{
    Task<IEnumerable<BlogPost>> GetPublishedPostsAsync();
    Task<(IEnumerable<BlogPost> Items, int TotalCount)> GetPublishedPostsPaginatedAsync(int pageNumber, int pageSize);
    Task<BlogPost?> GetBySlugAsync(string slug);
    Task<BlogPost?> GetByIdAsync(int id);
    Task<IEnumerable<BlogPost>> GetAllPostsAsync();
    Task AddAsync(BlogPost post);
    Task UpdateAsync(BlogPost post);
    Task DeleteAsync(int id);
}