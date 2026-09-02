namespace ClubPOS.Core.Entities;

public class Membership : BaseEntity
{
    public int? CreditCardTypeId { get; set; }
    public int MembershipTypeId { get; set; }
    public int? MembershipNum { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? Country { get; set; }
    public string? PostalZipCode { get; set; }
    public DateTime MembershipStartDate { get; set; }
    public DateTime? MembershipEndDate { get; set; }
    public string? CreditCardNumber { get; set; }
    public DateTime? CreditCardExpiry { get; set; }
    public int? Csv { get; set; }
    public bool IsActive { get; set; }

    public virtual CreditCardType? CreditCardType { get; set; }
    public virtual MembershipType? MembershipType { get; set; }
    public virtual ICollection<BookingDetail>? BookingDetails { get; set; }
    public virtual ICollection<Problem>? Problems { get; set; }
    // public virtual ICollection<Reservation> Reservations { get; set; }
    public virtual ICollection<Transaction>? Transactions { get; set; }
    public virtual ICollection<WaitingList>? WaitingLists { get; set; }
}