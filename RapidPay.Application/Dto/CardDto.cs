using System.ComponentModel.DataAnnotations;

namespace RapidPay.Application.Dto;

public record CardDto([Required, MaxLength(15), MinLength(15)]string Number, [Required, Range(0.00, double.MaxValue)]decimal Balance);