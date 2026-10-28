using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosEnterprise.Core.Interfaces;
using System.Text;
using System.Threading.Tasks;

namespace PosEnterprise.Web.Pages;

[IgnoreAntiforgeryToken]
public class SitemapModel : PageModel
{
    private readonly ISitemapService _sitemapService;

    public SitemapModel(ISitemapService sitemapService)
    {
        _sitemapService = sitemapService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var xmlContent = await _sitemapService.GenerateSitemapXmlAsync(baseUrl);

        return Content(xmlContent, "application/xml; charset=utf-8", Encoding.UTF8);
    }
}