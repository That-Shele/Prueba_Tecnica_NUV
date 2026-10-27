using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosEnterprise.Core.Entities;
using PosEnterprise.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PosEnterprise.Web.Areas.Admin.Pages.Blog;

public class IndexModel : PageModel
{
    private readonly IBlogRepository _blogRepository;

    public IndexModel(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public IEnumerable<BlogPost> Articles { get; set; } = new List<BlogPost>();

    [TempData]
    public string? Message { get; set; }

    public async Task OnGetAsync()
    {
        Articles = await _blogRepository.GetAllPostsAsync();
    }
}