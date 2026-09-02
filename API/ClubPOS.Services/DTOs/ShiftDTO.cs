using ClubPOS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPOS.Services.DTOs
{
    public class ShiftDTO
    {
        public int ShiftId { get; set; }
        public int ShiftNumber { get; set; }
        public string? ShiftStart { get; set; }
        public string? ShiftEnd { get; set; }
        public string? UserId { get; set; }
        public int OrganizationId { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<SalesSession>? SalesSessions { get; set; }
    }
}
