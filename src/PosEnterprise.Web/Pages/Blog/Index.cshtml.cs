using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosEnterprise.Core.Entities;
using PosEnterprise.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PosEnterprise.Web.Pages.Blog;

public class IndexModel : PageModel
{
    private readonly IBlogRepository _blogRepository;

    public IndexModel(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public IEnumerable<BlogPost> Articles { get; set; } = new List<BlogPost>();
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 1;
    public int PageSize { get; set; } = 6;
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;

    public async Task OnGetAsync([FromQuery] int p = 1)
    {
        CurrentPage = p < 1 ? 1 : p;

        var (items, totalCount) = await _blogRepository.GetPublishedPostsPaginatedAsync(CurrentPage, PageSize);

        TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

        if (TotalPages > 0 && CurrentPage > TotalPages)
        {
            CurrentPage = TotalPages;
            (items, _) = await _blogRepository.GetPublishedPostsPaginatedAsync(CurrentPage, PageSize);
        }

        Articles = items;
    }
}