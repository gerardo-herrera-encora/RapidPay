using System.Collections.Concurrent;
using RapidPay.Domain.Entities;
using RapidPay.Infrastructure.Interfaces;

namespace RapidPay.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly ConcurrentDictionary<string, Card> _cards = new();

    public async Task AddAsync(Card card)
    {
        await Task.Delay(1000); // Simulate delay
        if (!_cards.TryAdd(card.Number, card))
        {
            throw new InvalidOperationException("Card already exists.");
        }
    }

    public async Task UpdateAsync(Card card)
    {
        await Task.Delay(1000); // Simulate delay
        if (!_cards.ContainsKey(card.Number))
        {
            throw new KeyNotFoundException("Card not found.");
        }
        _cards[card.Number] = card;
    }

    public async Task<Card?> GetByNumberAsync(string number)
    {
        await Task.Delay(1000); // Simulate delay
        _cards.TryGetValue(number, out var card);
        return card;
    }
}
