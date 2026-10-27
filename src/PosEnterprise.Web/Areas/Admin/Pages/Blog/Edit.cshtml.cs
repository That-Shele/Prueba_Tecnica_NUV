using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosEnterprise.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
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
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(200)]
        public string Slug { get; set; } = string.Empty;

        [Required, StringLength(500)]
        public string Summary { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Author { get; set; } = string.Empty;

        public bool IsPublished { get; set; }

        [StringLength(200)]
        public string? MetaTitle { get; set; }

        [StringLength(300)]
        public string? MetaDescription { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var post = await _blogRepository.GetByIdAsync(id);
        if (post == null) return NotFound();

        Input = new InputModel
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
        if (!ModelState.IsValid) return Page();

        var post = await _blogRepository.GetByIdAsync(Input.Id);
        if (post == null) return NotFound();

        post.Title = Input.Title;
        post.Slug = Input.Slug.ToLower().Trim().Replace(" ", "-");
        post.Summary = Input.Summary;
        post.Content = Input.Content;
        post.Author = Input.Author;
        post.IsPublished = Input.IsPublished;
        post.MetaTitle = Input.MetaTitle ?? Input.Title;
        post.MetaDescription = Input.MetaDescription ?? Input.Summary;

        await _blogRepository.UpdateAsync(post);

        TempData["Message"] = "Artículo actualizado exitosamente.";
        return RedirectToPage("/Blog/Index");
    }
}