using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosEnterprise.Core.Entities;
using PosEnterprise.Core.Interfaces;
using System.Threading.Tasks;

namespace PosEnterprise.Web.Areas.Admin.Pages.Blog;

public class DeleteModel : PageModel
{
    private readonly IBlogRepository _blogRepository;

    public DeleteModel(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    [BindProperty]
    public BlogPost Article { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var post = await _blogRepository.GetByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        Article = post;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        await _blogRepository.DeleteAsync(id);
        TempData["Message"] = "El artículo ha sido eliminado permanentemente.";
        return RedirectToPage("/Blog/Index");
    }
}