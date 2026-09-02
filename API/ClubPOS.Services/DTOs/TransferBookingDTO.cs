using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPOS.Services.DTOs
{
    public class TransferBookingDTO
    {
        //public int AccommodationId { get; set; }
        //public int BookedAccommodationId { get; set; }
        public decimal RateTotal { get; set; }
        public decimal RateDiscount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TotalDeposits { get; set; }
        public decimal Total { get; set; }
        public bool? IsTransfer { get; set; }
        //public string? UserId { get; set; }
        //public int OrganizationId { get; set; }
    }
}
