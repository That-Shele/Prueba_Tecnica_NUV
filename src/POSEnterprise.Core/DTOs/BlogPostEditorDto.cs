using System.ComponentModel.DataAnnotations;

namespace PosEnterprise.Core.DTOs;

public class BlogPostEditorDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "El Slug URL es obligatorio.")]
    [StringLength(200, ErrorMessage = "El Slug no puede exceder 200 caracteres.")]
    public string Slug { get; set; } = string.Empty;

    [Required(ErrorMessage = "El resumen es obligatorio.")]
    [StringLength(500, ErrorMessage = "El resumen no puede exceder 500 caracteres.")]
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