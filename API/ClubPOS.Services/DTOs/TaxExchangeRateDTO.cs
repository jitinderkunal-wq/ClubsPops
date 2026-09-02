using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPOS.Services.DTOs
{
    public class TaxExchangeRateDTO
    {
        public decimal Gst { get; set; }
        public decimal Hst { get; set; }
        public decimal UsExchange { get; set; }
        public decimal EuroExchange { get; set; }
        public string? UserId { get; set; }
        public int OrganizationId { get; set; }
    } 
}
