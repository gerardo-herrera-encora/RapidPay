using RapidPay.Application.Dto;
using RapidPay.Application.Interfaces;
using RapidPay.Domain.Entities;
using RapidPay.Infrastructure.Interfaces;

namespace RapidPay.Application.Services;

public class CardManagementService(
    ICardRepository cardRepository,
    IUniversalFeesExchangeService universalFeesExchangeService) : ICardManagementService
{
    public async Task<CardDto> CreateCardAsync(CardDto cardDto)
    {
        var card = new Card(cardDto.Number, cardDto.Balance);
        await cardRepository.AddAsync(card);
        return new CardDto(card.Number, card.Balance);
    }

    public async Task<decimal?> PayAsync(string number, decimal amount)
    {
        var card = await cardRepository.GetByNumberAsync(number);
        if (card == null)
        {
            return null;
        }
        card.Pay(amount + universalFeesExchangeService.CalculateFee());
        await cardRepository.UpdateAsync(card);
        return card.Balance;
    }

    public async Task<decimal?> GetCardBalanceAsync(string number)
    {
        var card = await cardRepository.GetByNumberAsync(number);
        return card?.Balance;
    }
}