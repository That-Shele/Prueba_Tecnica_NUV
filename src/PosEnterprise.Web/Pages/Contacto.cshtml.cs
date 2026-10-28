using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosEnterprise.Core.DTOs;
using PosEnterprise.Core.Entities;
using PosEnterprise.Core.Interfaces;
using System;
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
    public ContactFormDto Input { get; set; } = new();

    [TempData]
    public string? SuccessMessage { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Mapeo de DTO a Entidad de Dominio
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

        SuccessMessage = "Gracias por tu consulta. Un asesor comercial especializado se pondrá en contacto a la brevedad.";
        return RedirectToPage("/Contacto");
    }
}