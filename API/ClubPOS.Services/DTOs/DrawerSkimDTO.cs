using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPOS.Services.DTOs
{
    public class DrawerSkimDTO
    {
        //public int DrawerSkimId { get; set; }
        //public DateTime SkimDate { get; set; }
        //public int? SkimShiftCount { get; set; }
        //public decimal? TotalCoin { get; set; }

        public string? UserId { get; set; }
        public int SalesSessionId { get; set; } 
        public int OrganizationId { get; set; } 

        public int Nickels { get; set; }
        public int Dimes {get;set;}
        public int Quarters {get;set;}
        public int Qty1Dollar { get; set; }
        public int Qty2Dollar { get; set; }
        public int Qty5Dollar { get; set; }
        public int Qty10Dollar { get; set; }
        public int Qty20Dollar { get; set; }
        public int Qty50Dollar { get; set; }
        public int Qty100Dollar { get; set; }
        public decimal CashDepositTotal { get; set; } 


        public int Debit {get;set;}
        public int Visa {get;set;}
        public int MasterCard {get;set;}
        public int Amex {get;set;}
        public int Discover {get;set;}
        public int OtherCreditCard {get;set;}
        public int UsDollars {get;set;}
        public int EuroDollars {get;set;}
        public decimal DebitCreditTotal { get; set; }


        public decimal GrandTotal { get; set; }


        //public decimal? TotalCheques { get; set; }
        //public decimal? TotalOther { get; set; }
        //public decimal? TotalEuros { get; set; }
        //public int? Qty1DollarUs { get; set; }
        //public int? Qty2DollarUs { get; set; }
        //public int? Qty5DollarUs { get; set; }
        //public int? Qty10DollarUs { get; set; }
        //public int? Qty20DollarUs { get; set; }
        //public int? Qty50DollarUs { get; set; }
        //public int? Qty100DollarUs { get; set; }
        //public decimal CoinTotal { get; set; }
        //public decimal Total1Dollar { get; set; }
        //public decimal Total2Dollar { get; set; }
        //public decimal Total5Dollar { get; set; }
        //public decimal Total10Dollar { get; set; }
        //public decimal Total20Dollar { get; set; }
        //public decimal Total50Dollar { get; set; }
        //public decimal Total100Dollar { get; set; }
        //public decimal Total1DollarUs { get; set; }
        //public decimal Total2DollarUs { get; set; }
        //public decimal Total5DollarUs { get; set; }
        //public decimal Total10DollarUs { get; set; }
        //public decimal Total20DollarUs { get; set; }
        //public decimal Total50DollarUs { get; set; }
        //public decimal Total100DollarUs { get; set; }
        //public decimal ChequeTotal { get; set; }
        //public decimal OtherTotal { get; set; }
        //public decimal EuroTotal { get; set; }
        //public decimal TotalUsdollars { get; set; }
        //public decimal TotalCaddollar { get; set; }
        //public int SalesSessionId { get; set; }

    }
}
