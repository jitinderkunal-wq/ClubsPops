using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPOS.Services.DTOs
{
    public class CashReconcilationDTO
    {
        public int SalesSessionId { get; set; }
        public int CashierReconciliationId { get; set; } 
        public decimal AccommodationSalesPolled { get; set; }
        public decimal RefundAccommodationPolled { get; set; } 
        public decimal VoidedAccommodationsPolled { get; set; }
        public decimal RateDiscountsPolled { get; set; }
        public decimal CouponDiscountsPolled { get; set; }
        public decimal PromoDiscountsPolled { get; set; }  
        public decimal OtherDiscountsRolled { get; set; }
        public decimal MemberDiscountsPolled { get; set; }
        public decimal TotalAccommodationSalesPolled { get; set; }

        public decimal DepositsCollectedPolled { get; set; }
        public decimal DepositsReturnedPolled { get; set; }
        public decimal MembershipSalesPolled { get; set; }
        public decimal OtherSalesPolled { get; set; }
        public decimal BarSalesPolled { get; set; }
        public decimal TotalFinalSalesPolled { get; set; } 

        public decimal DebitPolled { get; set; }
        public decimal MasterCardPolled { get; set; }
        public decimal VisaPolled { get; set; }
        public decimal AmexPolled { get; set; }
        public decimal DiscoverPolled { get; set; }
        public decimal OtherCreditCardPolled { get; set; } 
        public decimal TotalDebitCreditPolled { get; set; }

        public decimal EosDebitCreditTotalPolled { get; set; }

        public decimal TotalUsdollarsPolled { get; set; }
        public decimal TotalEuroDollarsPolled { get; set; }

        public decimal FinalCashDepositPolled { get; set; }
        public decimal PaidInsPolled { get; set; }
        public decimal ReceiptsPaidOutsPolled { get; set; }
        public decimal CashDropsTotalPolled { get; set; }
        public decimal CashOverShortPolled { get; set; }


        //Accommodation
        public int LockerQuantity { get; set; }
        public int SingleRoomQuantity { get; set; }
        public int SingleRoomTVQuantity { get; set; }
        public int DoubleRoomTVQuantity { get; set; }
        public decimal LockerAccomodationTotal { get; set; }
        public decimal SingleRoomAccomodationTotal { get; set; }
        public decimal SingleRoomTVAccomodationTotal { get; set; }
        public decimal DoubleRoomTVAccomodationTotal { get; set; }


        //Transfer
        public int LockerTransferQuantity { get; set; }
        public int SingleRoomTransferQuantity { get; set; }
        public int SingleRoomTVTransferQuantity { get; set; }
        public int DoubleRoomTVTransferQuantity { get; set; }
        public decimal LockerAccomodationTransferTotal { get; set; }
        public decimal SingleRoomAccomodationTransferTotal { get; set; }
        public decimal SingleRoomTVAccomodationTransferTotal { get; set; }
        public decimal DoubleRoomTVAccomodationTransferTotal { get; set; }

        //Deposits
        public decimal RefundedDeposits { get; set; }
        public decimal ForfittedDeposist { get; set; }
        public decimal TotalCollected { get; set; }

        public string? UserId { get; set; }
        public int OrganizationId { get; set; }







        //public int CashierReconciliationId { get; set; }
        //public DateTime DateCreated { get; set; }
        //public decimal AccommodationSales { get; set; }
        //public decimal VoidedAccommodations { get; set; }
        //public decimal RateDiscounts { get; set; }
        //public decimal CouponDiscounts { get; set; }
        //public decimal Promodiscouunts { get; set; }
        //public decimal OtherDiscounts { get; set; }
        //public decimal MemberDiscounts { get; set; }
        //public decimal TotalAccommodationSales { get; set; }
        //public decimal DepositsCollected { get; set; }
        //public decimal DepositsReturned { get; set; }
        //public decimal MembershipSales { get; set; }
        //public decimal OtherSales { get; set; }
        //public decimal BarSales { get; set; }
        //public decimal FinalSalesTotal { get; set; }
        //public decimal Debit { get; set; }
        //public decimal MasterCard { get; set; }
        //public decimal Visa { get; set; }
        //public decimal Amex { get; set; }
        //public decimal Discover { get; set; }
        //public decimal OtherCreditCard1 { get; set; }
        //public decimal TotalDebitCredit { get; set; }
        //public decimal UsExchangeRatePolled { get; set; }
        //public decimal EuroExchangeRatePolled { get; set; }
        //public decimal UsExchangeRate { get; set; }
        //public decimal EuroExchangeRate { get; set; }
        //public decimal TotalUsdollars { get; set; }
        //public decimal TotalEuroDollars { get; set; }
        //public decimal PaidIns { get; set; }
        //public decimal FinalCashDeposit { get; set; }
        //public decimal ReceiptsPaidOuts { get; set; }
        //public decimal TotalCashDrops { get; set; }
        //public decimal CashOverShort { get; set; }
        //public decimal CashDrop1Polled { get; set; }
        //public decimal CashDropTotalPolled { get; set; }
        //public decimal CashDrop2Polled { get; set; }
        //public decimal CashDrop2 { get; set; }
        //public decimal CashDrop3Polled { get; set; }
        //public decimal CashDrop3 { get; set; }
        //public decimal CashDrop4Polled { get; set; }
        //public decimal CashDrop4 { get; set; }
        //public decimal CashDrop5Polled { get; set; }
        //public decimal CashDrop5 { get; set; }
        //public decimal CashDrop6Polled { get; set; }
        //public decimal CashDrop6 { get; set; }
        //public int TransactionCount { get; set; }
        //public int AccommodationSalesCount { get; set; }
        //public int VoidedAccommodationsCount { get; set; }
        //public int RateDiscountsCount { get; set; }
        //public int CouponDiscountsCount { get; set; }
        //public int PromodiscouuntsCount { get; set; }
        //public int OtherDiscountsCount { get; set; }
        //public int MemberDiscountsCount { get; set; }
        //public int DepositsCollectedCount { get; set; }
        //public int DepositsReturnedCount { get; set; }
        //public int MembershipSalesCount { get; set; }
        //public int OtherSalesCount { get; set; }
        //public int BarSalesCount { get; set; }
        //public int FinalSalesTotalCount { get; set; }
        //public int DebitCount { get; set; }
        //public int MasterCardCount { get; set; }
        //public int Visacount { get; set; }
        //public int Amexcount { get; set; }
        //public int DiscoverCount { get; set; }
        //public int OtherCreditCard1Count { get; set; }
        //public int TotalDebitCreditCount { get; set; }
        //public int TotalUsdollarsCount { get; set; }
        //public int TotalEuroDollarsCount { get; set; }
        //public int CashCount { get; set; }
        //public decimal Cash { get; set; }
        //public int FinalCashDepositCount { get; set; }
        //public int PaidInsCount { get; set; }
        //public int ReceiptsPaidOutsCount { get; set; }
        //public bool IsReconciled { get; set; }
        //public int Eosnickels { get; set; }
        //public int Eosdimes { get; set; }
        //public int Eosquarters { get; set; }
        //public int Eosloonies { get; set; }
        //public int Eostwonies { get; set; }
        //public int Eosfives { get; set; }
        //public int Eostens { get; set; }
        //public int Eostwentys { get; set; }
        //public int Eosfiftys { get; set; }
        //public int EosoneHundreds { get; set; }
        //public decimal EosdepositTotal { get; set; }
        //public decimal Eosdebit { get; set; }
        //public decimal Eosvisa { get; set; }
        //public decimal EosmasterCard { get; set; }
        //public decimal Eosamex { get; set; }
        //public decimal Eosdiscover { get; set; }
        //public decimal EosotherCreditCard1 { get; set; }
        //public decimal Eosusdollars { get; set; }
        //public decimal EoseuroDollars { get; set; }
        //public decimal Eoscheques { get; set; }
        //public decimal EosfinalTotal { get; set; }
        //public decimal EosquickCashDeposit { get; set; }
        //public decimal EosquickDebitCredit { get; set; }
        //public int SalesSessionId { get; set; }
        //public DateTime SalesStartTime { get; set; }
        //public DateTime? SalesEndTime { get; set; }
        //public decimal EosusdollarsConverted { get; set; }
        //public decimal EoseuroDollarsConverted { get; set; }
        //public int CashDropCount { get; set; }
        //public decimal RefundsTotalCountPolled { get; set; }
        //public decimal RefundsTotal { get; set; }

        //public virtual SalesSession SalesSession { get; set; } = null!;

    }
}
