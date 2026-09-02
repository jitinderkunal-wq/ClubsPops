namespace ClubPOS.Core.Entities;

public class DrawerSkim : BaseEntity
{
    public int SalesSessionId { get; set; }
    public DateTime SkimDate { get; set; }
    public int? SkimShiftCount { get; set; }
    public decimal? TotalCoin { get; set; }
    public int? Qty1Dollar { get; set; }
    public int? Qty2Dollar { get; set; }
    public int? Qty5Dollar { get; set; }
    public int? Qty10Dollar { get; set; }
    public int? Qty20Dollar { get; set; }
    public int? Qty50Dollar { get; set; }
    public int? Qty100Dollar { get; set; }
    public decimal? TotalCheques { get; set; }
    public decimal? TotalOther { get; set; }
    public decimal? TotalEuros { get; set; }
    public int? Qty1DollarUs { get; set; }
    public int? Qty2DollarUs { get; set; }
    public int? Qty5DollarUs { get; set; }
    public int? Qty10DollarUs { get; set; }
    public int? Qty20DollarUs { get; set; }
    public int? Qty50DollarUs { get; set; }
    public int? Qty100DollarUs { get; set; }
    public decimal CoinTotal { get; set; }
    public decimal Total1Dollar { get; set; }
    public decimal Total2Dollar { get; set; }
    public decimal Total5Dollar { get; set; }
    public decimal Total10Dollar { get; set; }
    public decimal Total20Dollar { get; set; }
    public decimal Total50Dollar { get; set; }
    public decimal Total100Dollar { get; set; }
    public decimal Total1DollarUs { get; set; }
    public decimal Total2DollarUs { get; set; }
    public decimal Total5DollarUs { get; set; }
    public decimal Total10DollarUs { get; set; }
    public decimal Total20DollarUs { get; set; }
    public decimal Total50DollarUs { get; set; }
    public decimal Total100DollarUs { get; set; }
    public decimal ChequeTotal { get; set; }
    public decimal OtherTotal { get; set; }
    public decimal EuroTotal { get; set; }
    public decimal TotalUsdollars { get; set; }
    public decimal TotalCaddollar { get; set; }
    public decimal GrandTotal { get; set; }

    public virtual SalesSession? SalesSession { get; set; }
}