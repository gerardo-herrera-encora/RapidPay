using RapidPay.Application.Dto;

namespace RapidPay.Application.Interfaces;

public interface ICardManagementService
{
    Task<CardDto> CreateCardAsync(CardDto cardDto);
    Task<decimal?> PayAsync(string number, decimal amount);
    Task<decimal?> GetCardBalanceAsync(string number);
}