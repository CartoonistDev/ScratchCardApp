namespace ScratchCard.Models;

public class ScratchCards
{
    public int Id { get; set; }
    public string Code { get; set; }
    public bool IsPurchased { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public DateTime? UsageDate { get; set; }
}
