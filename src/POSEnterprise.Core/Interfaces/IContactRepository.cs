using PosEnterprise.Core.Entities;

namespace PosEnterprise.Core.Interfaces;

public interface IContactRepository
{
    Task AddAsync(ContactRequest request);
    Task<IEnumerable<ContactRequest>> GetAllAsync();
}