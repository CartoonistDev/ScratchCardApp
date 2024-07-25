using ScratchCard.IServices;
using ScratchCard.Repository;

namespace ScratchCard.Services;

public class ScratchCardService : IScratchCardService
{
    private readonly IScratchCardRepository _repository;

    public ScratchCardService(IScratchCardRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Models.ScratchCards>> GenerateCardsAsync(int count)
    {
        var cards = new List<Models.ScratchCards>();
        for (int i = 0; i < count; i++)
        {
            var card = new Models.ScratchCards { Code = Guid.NewGuid().ToString() };
            await _repository.AddAsync(card);
            cards.Add(card);
        }
        await _repository.SaveChangesAsync();
        return cards;
    }

    public async Task<IEnumerable<Models.ScratchCards>> ListCardsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Models.ScratchCards> PurchaseCardAsync(int id)
    {
        try
        {
            if (id == 0)
            {
                throw new ArgumentException("Id cannot be zero {0}");
            }
            var card = await _repository.GetByIdAsync(id) ?? throw new ArgumentException("Card not found");

            if (card.IsPurchased)
            {
                throw new InvalidOperationException("Card is already purchased");
            }

            card.IsPurchased = true;
            card.PurchaseDate = DateTime.UtcNow;
            await _repository.UpdateAsync(card);
            await _repository.SaveChangesAsync();
            return card;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<Models.ScratchCards> UseCardAsync(int id)
    {
        try
        {
            if (id == 0)
            {
                throw new ArgumentException("Id cannot be zero {0}");
            }

            var card = await _repository.GetByIdAsync(id) ?? throw new ArgumentException("Card not found");

            if (!card.IsPurchased)
            {
                throw new InvalidOperationException("Card has not been purchased");
            }
            if (card.IsUsed)
            {
                throw new InvalidOperationException("Card is already used");
            }

            card.IsUsed = true;
            card.UsageDate = DateTime.UtcNow;
            await _repository.UpdateAsync(card);
            await _repository.SaveChangesAsync();
            return card;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
