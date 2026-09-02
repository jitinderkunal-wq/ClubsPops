namespace ClubPOS.Core.Entities;

public class Tax : BaseEntity
{
    public string? TaxName { get; set; }
    public decimal TaxRate { get; set; }

    public virtual ICollection<AccommodationRate>? AccommodationRateTax1Navigations { get; set; }
    public virtual ICollection<AccommodationRate>? AccommodationRateTax2Navigations { get; set; }
    public virtual ICollection<AccommodationRate>? AccommodationRateTax3Navigations { get; set; }
    public virtual ICollection<AccommodationRate>? AccommodationRateTax4Navigations { get; set; }
    public virtual ICollection<AccommodationRate>? AccommodationRateTax5Navigations { get; set; }
    // public virtual ICollection<Sundry> Sundries { get; set; }
}