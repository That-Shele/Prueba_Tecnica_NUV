using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PosEnterprise.Web.TagHelpers;

[HtmlTargetElement("link", Attributes = "asp-canonical")]
public class CanonicalTagHelper : TagHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CanonicalTagHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    [HtmlAttributeName("asp-canonical")]
    public bool Enabled { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (!Enabled) return;

        var request = _httpContextAccessor.HttpContext?.Request;
        if (request != null)
        {
            var canonicalUrl = $"{request.Scheme}://{request.Host}{request.Path}";
            output.Attributes.SetAttribute("rel", "canonical");
            output.Attributes.SetAttribute("href", canonicalUrl);
        }
    }
}