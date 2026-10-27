using PosEnterprise.Core.Entities;

namespace PosEnterprise.Core.Interfaces;

public interface IBlogRepository
{
	Task<IEnumerable<BlogPost>> GetPublishedPostsAsync();
	Task<BlogPost?> GetBySlugAsync(string slug);
	Task<BlogPost?> GetByIdAsync(int id);
	Task<IEnumerable<BlogPost>> GetAllPostsAsync(); // Para el área administrativa
	Task AddAsync(BlogPost post);
	Task UpdateAsync(BlogPost post);
	Task DeleteAsync(int id);
}