using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosEnterprise.Core.Entities;
using PosEnterprise.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PosEnterprise.Web.Areas.Admin.Pages.Blog;

public class CreateModel : PageModel
{
    private readonly IBlogRepository _blogRepository;

    public CreateModel(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Slug URL es obligatorio.")]
        [StringLength(200)]
        public string Slug { get; set; } = string.Empty;

        [Required(ErrorMessage = "El resumen es obligatorio.")]
        [StringLength(500)]
        public string Summary { get; set; } = string.Empty;

        [Required(ErrorMessage = "El contenido es obligatorio.")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "El autor es obligatorio.")]
        [StringLength(100)]
        public string Author { get; set; } = "Equipo POS Enterprise";

        public bool IsPublished { get; set; } = true;

        [StringLength(200)]
        public string? MetaTitle { get; set; }

        [StringLength(300)]
        public string? MetaDescription { get; set; }
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var post = new BlogPost
        {
            Title = Input.Title,
            Slug = Input.Slug.ToLower().Trim().Replace(" ", "-"),
            Summary = Input.Summary,
            Content = Input.Content,
            Author = Input.Author,
            IsPublished = Input.IsPublished,
            MetaTitle = string.IsNullOrEmpty(Input.MetaTitle) ? Input.Title : Input.MetaTitle,
            MetaDescription = string.IsNullOrEmpty(Input.MetaDescription) ? Input.Summary : Input.MetaDescription,
            PublishedAt = DateTime.UtcNow
        };

        await _blogRepository.AddAsync(post);

        TempData["Message"] = "Artículo creado exitosamente.";
        return RedirectToPage("/Blog/Index");
    }
}