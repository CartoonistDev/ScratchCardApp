using Microsoft.EntityFrameworkCore;
using ScratchCard.Data;

namespace ScratchCard.Repository;

public class ScratchCardRepository : IScratchCardRepository
{
    private readonly AppDbContext _context;

    public ScratchCardRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Models.ScratchCards>> GetAllAsync()
    {
        return await _context.ScratchCards.ToListAsync();
    }

    public async Task<Models.ScratchCards> GetByIdAsync(int id)
    {
        return await _context.ScratchCards.FindAsync(id);
    }

    public async Task AddAsync(Models.ScratchCards card)
    {
        await _context.ScratchCards.AddAsync(card);
    }

    public async Task UpdateAsync(Models.ScratchCards card)
    {
        _context.ScratchCards.Update(card);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

