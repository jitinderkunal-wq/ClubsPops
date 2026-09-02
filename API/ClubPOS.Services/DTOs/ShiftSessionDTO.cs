using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPOS.Services.DTOs
{
    public class ShiftSessionDTO
    {
        public int ShiftSessionId { get; set; } 
        public string? UserId { get; set; }   
        public DateTime SessionDateCreated { get; set; }
        public DateTime? SessionEndDate { get; set; }
        public int ShiftNumber { get; set; }
        public decimal BeginFloat { get; set; }
        public int DrawerNumber { get; set; }
        public DateTime BusinessDay { get; set; }
        public bool IsCurrentlyInUse { get; set; }
        public int OrganizationId { get; set; }
    }
}
