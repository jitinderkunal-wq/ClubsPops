namespace ClubPOS.Core.Entities;

public class SalesType : BaseEntity
{
    public string? SalesTypeName { get; set; }

    public virtual ICollection<Transaction>? Transactions { get; set; }
}