namespace PosEnterprise.Core.Entities;

public class ContactRequest
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string BusinessType { get; set; } = string.Empty; // "Restaurante", "Tienda / Retail", "Otro"
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsAddressed { get; set; } = false;
}