using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosEnterprise.Core.DTOs;
using PosEnterprise.Core.Entities;
using PosEnterprise.Core.Interfaces;
using System;
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
    public BlogPostEditorDto Input { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Mapeo de DTO a Entidad de Dominio
        var post = new BlogPost
        {
            Title = Input.Title,
            Slug = Input.Slug.ToLower().Trim().Replace(" ", "-"),
            Summary = Input.Summary,
            Content = Input.Content,
            Author = Input.Author,
            IsPublished = Input.IsPublished,
            MetaTitle = string.IsNullOrWhiteSpace(Input.MetaTitle) ? Input.Title : Input.MetaTitle,
            MetaDescription = string.IsNullOrWhiteSpace(Input.MetaDescription) ? Input.Summary : Input.MetaDescription,
            PublishedAt = DateTime.UtcNow
        };

        await _blogRepository.AddAsync(post);

        TempData["Message"] = "Artículo publicado correctamente.";
        return RedirectToPage("/Blog/Index");
    }
}