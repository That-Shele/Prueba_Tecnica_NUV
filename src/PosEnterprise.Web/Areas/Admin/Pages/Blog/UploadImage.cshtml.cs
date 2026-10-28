using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PosEnterprise.Web.Areas.Admin.Pages.Blog;

[IgnoreAntiforgeryToken]
public class UploadImageModel : PageModel
{
    private readonly IWebHostEnvironment _environment;

    public UploadImageModel(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<IActionResult> OnPostAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { errorMessage = "No se ha seleccionado ninguna imagen." });
        }

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
        {
            return BadRequest(new { errorMessage = "Formato no válido. Usa JPG, PNG, WEBP o GIF." });
        }

        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "blog");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var imageUrl = $"/uploads/blog/{uniqueFileName}";

        // Estructura JSON estándar para SunEditor y editores WYSIWYG
        return new JsonResult(new { result = new[] { new { url = imageUrl, name = file.FileName } } });
    }
}