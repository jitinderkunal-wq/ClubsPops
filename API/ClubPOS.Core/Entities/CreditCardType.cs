namespace ClubPOS.Core.Entities;

public class CreditCardType : BaseEntity
{
    public string? CreditCardTypeName { get; set; }

    public virtual ICollection<Membership>? Memberships { get; set; }
}