namespace ClubPOS.Core.Entities;

public class ExchangeRate : BaseEntity
{
    public string? RateName { get; set; }
    public decimal Rate { get; set; }
}