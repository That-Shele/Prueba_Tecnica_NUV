using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PosEnterprise.Web.Pages;

public class RobotsTxtModel : PageModel
{
    public IActionResult OnGet()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var content = new StringBuilder();

        content.AppendLine("User-agent: *");
        content.AppendLine("Disallow: /Admin/");
        content.AppendLine("Disallow: /Areas/");
        content.AppendLine("Allow: /");
        content.AppendLine();
        content.AppendLine($"Sitemap: {baseUrl}/Sitemap");

        return Content(content.ToString(), "text/plain", Encoding.UTF8);
    }
}