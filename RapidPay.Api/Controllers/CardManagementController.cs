using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Application.Dto;
using RapidPay.Application.Interfaces;

namespace RapidPay.Api.Controllers;

/// <summary>
/// The card management module includes three API endpoints
/// </summary>
/// <param name="cardManagementService"></param>
[ApiController]
[Route("[controller]")]
[Authorize]
public class CardManagementController(ICardManagementService cardManagementService) : ControllerBase
{
    /// <summary>
    /// Create card (card format is 15 digits)
    /// </summary>
    /// <param name="cardDto">data for new card</param>
    /// <returns>Created card data</returns>
    [HttpPost]
    public async Task<IActionResult> CreateCard([FromBody] CardDto cardDto)
    {
        try
        {
            var newCard = await cardManagementService.CreateCardAsync(cardDto);
            return CreatedAtAction(nameof(CreateCard), new { number = newCard.Number }, newCard);
        }
        catch (InvalidOperationException)
        {
            return Conflict("Card already exists.");
        }
    }

    /// <summary>
    /// Pay (using the created card, and update balance)
    /// </summary>
    /// <param name="number">Card Number for the payment</param>
    /// <param name="amount">Payment amount</param>
    /// <returns>the card's new balance after the payment</returns>
    [HttpPatch("{number}/pay")]
    public async Task<IActionResult> Pay(string number, [FromBody, Range(0.01, double.MaxValue)] decimal amount)
    {
        var newBalance = await cardManagementService.PayAsync(number, amount);
        if (newBalance == null)
        {
            return NotFound();
        }
        return Ok(newBalance);
    }

    /// <summary>
    /// Get card balance
    /// </summary>
    /// <param name="number">Card Number for the search</param>
    /// <returns>the card current balance</returns>
    [HttpGet("{number}")]
    public async Task<IActionResult> GetCardBalance(string number)
    {
        var balance = await cardManagementService.GetCardBalanceAsync(number);
        if (balance == null)
        {
            return NotFound();
        }
        return Ok(balance);
    }
}
