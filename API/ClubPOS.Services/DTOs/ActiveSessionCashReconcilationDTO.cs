using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPOS.Services.DTOs
{
    public class ActiveSessionCashReconcilationDTO  
    {
        public int SalesSessionId { get; set; }
        public string? UserId { get; set; }
        public string? Name { get; set; } 
        public int Shift { get; set; } 
        public decimal BeginFloat { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
