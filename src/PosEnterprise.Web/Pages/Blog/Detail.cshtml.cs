using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosEnterprise.Core.Entities;
using PosEnterprise.Core.Interfaces;
using System.Threading.Tasks;

namespace PosEnterprise.Web.Pages.Blog;

public class DetailModel : PageModel
{
    private readonly IBlogRepository _blogRepository;

    public DetailModel(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public BlogPost Article { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        if (string.IsNullOrEmpty(slug))
        {
            return NotFound();
        }

        var article = await _blogRepository.GetBySlugAsync(slug);

        if (article == null)
        {
            return NotFound();
        }

        Article = article;
        return Page();
    }
}