using Microsoft.AspNetCore.Mvc;
using ScratchCard.IServices;


public class ScratchCardsController : Controller
{
    private readonly IScratchCardService _service;

    public ScratchCardsController(IScratchCardService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var cards = await _service.ListCardsAsync();
        return View(cards);
    }

    public async Task<IActionResult> Purchase(int id)
    {
        await _service.PurchaseCardAsync(id);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Use(int id)
    {
        await _service.UseCardAsync(id);
        return RedirectToAction("Index");
    }

    public IActionResult Generate()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Generate(int count)
    {
        await _service.GenerateCardsAsync(count);
        return RedirectToAction("Index");
    }
}
