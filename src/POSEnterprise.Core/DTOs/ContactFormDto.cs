using System.ComponentModel.DataAnnotations;

namespace PosEnterprise.Core.DTOs;

public class ContactFormDto
{
    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [StringLength(150, ErrorMessage = "El nombre no puede exceder 150 caracteres.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingrese una dirección de correo válida.")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Ingrese un número telefónico válido.")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Seleccione el tipo de negocio.")]
    public string BusinessType { get; set; } = string.Empty;

    [Required(ErrorMessage = "El mensaje o consulta es obligatorio.")]
    [StringLength(2000, ErrorMessage = "El mensaje no puede superar 2000 caracteres.")]
    public string Message { get; set; } = string.Empty;
}