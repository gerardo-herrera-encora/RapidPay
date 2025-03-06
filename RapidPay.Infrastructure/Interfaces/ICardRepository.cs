using RapidPay.Domain.Entities;

namespace RapidPay.Infrastructure.Interfaces;

public interface ICardRepository
{
    Task AddAsync(Card card);
    Task UpdateAsync(Card card);
    Task<Card?> GetByNumberAsync(string number);
}