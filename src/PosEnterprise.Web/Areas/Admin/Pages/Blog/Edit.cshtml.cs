using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosEnterprise.Core.DTOs;
using PosEnterprise.Core.Interfaces;
using System.Threading.Tasks;

namespace PosEnterprise.Web.Areas.Admin.Pages.Blog;

public class EditModel : PageModel
{
    private readonly IBlogRepository _blogRepository;

    public EditModel(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    [BindProperty]
    public BlogPostEditorDto Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var post = await _blogRepository.GetByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        // Mapeo de Entidad a DTO para edición
        Input = new BlogPostEditorDto
        {
            Id = post.Id,
            Title = post.Title,
            Slug = post.Slug,
            Summary = post.Summary,
            Content = post.Content,
            Author = post.Author,
            IsPublished = post.IsPublished,
            MetaTitle = post.MetaTitle,
            MetaDescription = post.MetaDescription
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var post = await _blogRepository.GetByIdAsync(Input.Id);
        if (post == null)
        {
            return NotFound();
        }

        // Actualización de valores desde el DTO
        post.Title = Input.Title;
        post.Slug = Input.Slug.ToLower().Trim().Replace(" ", "-");
        post.Summary = Input.Summary;
        post.Content = Input.Content;
        post.Author = Input.Author;
        post.IsPublished = Input.IsPublished;
        post.MetaTitle = string.IsNullOrWhiteSpace(Input.MetaTitle) ? Input.Title : Input.MetaTitle;
        post.MetaDescription = string.IsNullOrWhiteSpace(Input.MetaDescription) ? Input.Summary : Input.MetaDescription;

        await _blogRepository.UpdateAsync(post);

        TempData["Message"] = "Artículo actualizado exitosamente.";
        return RedirectToPage("/Blog/Index");
    }
}