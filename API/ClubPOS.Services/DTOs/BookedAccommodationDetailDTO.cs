using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPOS.Services.DTOs
{
    public class BookedAccommodationDetailDTO
    {
        public string? AccommodationNumber { get; set; }
        public string? AccommodationType { get; set; }
        public int HoursOfStay { get; set; }
        public int RateHours { get; set; }
        public string? CheckInTime { get; set; }
        public string? CheckOutTime { get; set; }
        public decimal TotalDepositCollected { get; set; }
        public decimal DepositsTransferred { get; set; }
        public decimal DepositsReturned { get; set; }
        public decimal DepositsBalance { get; set; }
        public decimal ChangeDue { get; set; }
        public string? Comment { get; set; }
    }
}
