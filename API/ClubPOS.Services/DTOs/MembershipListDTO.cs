using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPOS.Services.DTOs
{
    public class MembershipListDTO
    {
        //public int MembershipId { get; set; }
        public int MembershipTypeId { get; set; }
        public string? MembershipName { get; set; } 
    }
}
