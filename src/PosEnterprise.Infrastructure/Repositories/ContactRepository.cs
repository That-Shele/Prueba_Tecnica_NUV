using Microsoft.EntityFrameworkCore;
using PosEnterprise.Core.Entities;
using PosEnterprise.Core.Interfaces;
using PosEnterprise.Infrastructure.Data;

namespace PosEnterprise.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly ApplicationDbContext _context;

    public ContactRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ContactRequest request)
    {
        await _context.ContactRequests.AddAsync(request);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ContactRequest>> GetAllAsync()
    {
        return await _context.ContactRequests
            .AsNoTracking()
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }
}