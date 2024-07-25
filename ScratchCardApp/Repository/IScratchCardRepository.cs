namespace ScratchCard.Repository;

public interface IScratchCardRepository
{
    Task<IEnumerable<Models.ScratchCards>> GetAllAsync();
    Task<Models.ScratchCards> GetByIdAsync(int id);
    Task AddAsync(Models.ScratchCards card);
    Task UpdateAsync(Models.ScratchCards card);
    Task SaveChangesAsync();
}