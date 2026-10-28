using PosEnterprise.Core.Entities;
using System;
using System.Linq.Expressions;

namespace PosEnterprise.Core.Specifications;

public static class PublishedPostSpec
{
    public static Expression<Func<BlogPost, bool>> IsPublishedAndReady()
    {
        return post => post.IsPublished && post.PublishedAt <= DateTime.UtcNow;
    }
}