using ClubPOS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubPOS.Core.Entities.Common
{
    public class ResponseStatus
    {
        public string? Message { get; set; }
        public ResponseEnum Status { get; set; }
    }

    public enum ResponseEnum
    {
        Success,
        Error,
        Failed
    }

    public class LoginResponse : ResponseStatus
    {
        public UserDTO? UserDetail { get; set; }
    }
}
