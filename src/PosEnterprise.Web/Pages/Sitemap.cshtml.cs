using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosEnterprise.Core.Interfaces;
using System.Text;
using System.Threading.Tasks;

namespace PosEnterprise.Web.Pages;

public class SitemapModel : PageModel
{
    private readonly IBlogRepository _blogRepository;

    public SitemapModel(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var posts = await _blogRepository.GetPublishedPostsAsync();

        var xml = new StringBuilder();
        xml.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        xml.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

        // Páginas estáticas
        xml.AppendLine($"  <url><loc>{baseUrl}/</loc><priority>1.0</priority></url>");
        xml.AppendLine($"  <url><loc>{baseUrl}/Soluciones</loc><priority>0.8</priority></url>");
        xml.AppendLine($"  <url><loc>{baseUrl}/Blog</loc><priority>0.8</priority></url>");
        xml.AppendLine($"  <url><loc>{baseUrl}/Contacto</loc><priority>0.8</priority></url>");

        // Artículos dinámicos del blog
        foreach (var post in posts)
        {
            xml.AppendLine($"  <url>");
            xml.AppendLine($"    <loc>{baseUrl}/Blog/Detail?slug={post.Slug}</loc>");
            xml.AppendLine($"    <lastmod>{post.PublishedAt:yyyy-MM-dd}</lastmod>");
            xml.AppendLine($"    <priority>0.6</priority>");
            xml.AppendLine($"  </url>");
        }

        xml.AppendLine("</urlset>");

        return Content(xml.ToString(), "application/xml", Encoding.UTF8);
    }
}