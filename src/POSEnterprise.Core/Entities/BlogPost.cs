namespace PosEnterprise.Core.Entities;

public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Author { get; set; } = "Equipo POS Enterprise";
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    public bool IsPublished { get; set; } = true;

    // Metadatos SEO específicos para el artículo
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
}