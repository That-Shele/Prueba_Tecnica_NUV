using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosEnterprise.Core.Entities;
using PosEnterprise.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PosEnterprise.Web.Pages;

public class ContactoModel : PageModel
{
    private readonly IContactRepository _contactRepository;

    public ContactoModel(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    [TempData]
    public string? SuccessMessage { get; set; }

    public class InputModel
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

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var contactRequest = new ContactRequest
        {
            FullName = Input.FullName,
            Email = Input.Email,
            Phone = Input.Phone,
            BusinessType = Input.BusinessType,
            Message = Input.Message,
            CreatedAt = DateTime.UtcNow
        };

        await _contactRepository.AddAsync(contactRequest);

        SuccessMessage = "Gracias por contactarnos. Un especialista se comunicará contigo pronto.";
        return RedirectToPage("/Contacto");
    }
}