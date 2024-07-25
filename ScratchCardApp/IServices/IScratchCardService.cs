namespace ScratchCard.IServices;

public interface IScratchCardService
{
    Task<IEnumerable<Models.ScratchCards>> GenerateCardsAsync(int count);
    Task<IEnumerable<Models.ScratchCards>> ListCardsAsync();
    Task<Models.ScratchCards> PurchaseCardAsync(int id);
    Task<Models.ScratchCards> UseCardAsync(int id);
}