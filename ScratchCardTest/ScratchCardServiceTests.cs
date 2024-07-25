using FluentAssertions;
using Moq;
using ScratchCard.Models;
using ScratchCard.Repository;
using ScratchCard.Services;

namespace ScratchCardServiceTests;

public class ScratchCardServiceTests
{
    private readonly Mock<IScratchCardRepository> _repositoryMock;
    private readonly ScratchCardService _scratchCardService;

    public ScratchCardServiceTests()
    {
        _repositoryMock = new Mock<IScratchCardRepository>();
        _scratchCardService = new ScratchCardService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GenerateCardsAsync_ShouldReturnGeneratedCards()
    {
        // Arrange
        int cardCount = 5;

        // Act
        var result = await _scratchCardService.GenerateCardsAsync(cardCount);

        // Assert
        result.Should().HaveCount(cardCount);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<ScratchCards>()), Times.Exactly(cardCount));
        _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task ListCardsAsync_ShouldReturnAllCards()
    {
        // Arrange
        var cards = new List<ScratchCards>
        {
            new ScratchCards { Id = 1, Code = "Card1" },
            new ScratchCards { Id = 2, Code = "Card2" }
        };
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(cards);

        // Act
        var result = await _scratchCardService.ListCardsAsync();

        // Assert
        result.Should().BeEquivalentTo(cards);
        _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task PurchaseCardAsync_ShouldThrowArgumentException_WhenIdIsZero()
    {
        // Act
        Func<Task> act = async () => await _scratchCardService.PurchaseCardAsync(0);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Id cannot be zero {0}");
    }

    [Fact]
    public async Task PurchaseCardAsync_ShouldThrowArgumentException_WhenCardNotFound()
    {
        // Arrange
        int cardId = 1;

        _repositoryMock.Setup(r => r.GetByIdAsync(cardId)).ReturnsAsync((ScratchCards)null);

        // Act
        Func<Task> act = async () => await _scratchCardService.PurchaseCardAsync(cardId);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Card not found");
    }

    [Fact]
    public async Task PurchaseCardAsync_ShouldThrowInvalidOperationException_WhenCardIsAlreadyPurchased()
    {
        // Arrange
        int cardId = 1;
        var card = new ScratchCards { Id = cardId, IsPurchased = true };
        _repositoryMock.Setup(r => r.GetByIdAsync(cardId)).ReturnsAsync(card);

        // Act
        Func<Task> act = async () => await _scratchCardService.PurchaseCardAsync(cardId);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Card is already purchased");
    }

    [Fact]
    public async Task PurchaseCardAsync_ShouldPurchaseCard_WhenCardIsAvailable()
    {
        // Arrange
        int cardId = 1;
        var card = new ScratchCards { Id = cardId, IsPurchased = false };
        _repositoryMock.Setup(r => r.GetByIdAsync(cardId)).ReturnsAsync(card);

        // Act
        var result = await _scratchCardService.PurchaseCardAsync(cardId);

        // Assert
        result.Should().BeEquivalentTo(card, options => options.Excluding(c => c.IsPurchased).Excluding(c => c.PurchaseDate));
        result.IsPurchased.Should().BeTrue();
        result.PurchaseDate.Should().NotBeNull();
        _repositoryMock.Verify(r => r.UpdateAsync(card), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UseCardAsync_ShouldThrowArgumentException_WhenIdIsZero()
    {
        // Act
        Func<Task> act = async () => await _scratchCardService.UseCardAsync(0);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Id cannot be zero {0}");
    }

    [Fact]
    public async Task UseCardAsync_ShouldThrowArgumentException_WhenCardNotFound()
    {
        // Arrange
        int cardId = 1;
        _repositoryMock.Setup(r => r.GetByIdAsync(cardId)).ReturnsAsync((ScratchCards)null);

        // Act
        Func<Task> act = async () => await _scratchCardService.UseCardAsync(cardId);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Card not found");
    }

    [Fact]
    public async Task UseCardAsync_ShouldThrowInvalidOperationException_WhenCardIsNotPurchased()
    {
        // Arrange
        int cardId = 1;
        var card = new ScratchCards { Id = cardId, IsPurchased = false };
        _repositoryMock.Setup(r => r.GetByIdAsync(cardId)).ReturnsAsync(card);

        // Act
        Func<Task> act = async () => await _scratchCardService.UseCardAsync(cardId);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Card has not been purchased");
    }

    [Fact]
    public async Task UseCardAsync_ShouldThrowInvalidOperationException_WhenCardIsAlreadyUsed()
    {
        // Arrange
        int cardId = 1;
        var card = new ScratchCards { Id = cardId, IsPurchased = true, IsUsed = true };
        _repositoryMock.Setup(r => r.GetByIdAsync(cardId)).ReturnsAsync(card);

        // Act
        Func<Task> act = async () => await _scratchCardService.UseCardAsync(cardId);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Card is already used");
    }

    [Fact]
    public async Task UseCardAsync_ShouldUseCard_WhenCardIsAvailable()
    {
        // Arrange
        int cardId = 1;
        var card = new ScratchCards { Id = cardId, IsPurchased = true, IsUsed = false };
        _repositoryMock.Setup(r => r.GetByIdAsync(cardId)).ReturnsAsync(card);

        // Act
        var result = await _scratchCardService.UseCardAsync(cardId);

        // Assert
        result.Should().BeEquivalentTo(card, options => options.Excluding(c => c.IsUsed).Excluding(c => c.UsageDate));
        result.IsUsed.Should().BeTrue();
        result.UsageDate.Should().NotBeNull();
        _repositoryMock.Verify(r => r.UpdateAsync(card), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
