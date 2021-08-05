using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Models.Account
{
    public class RegisterResult
    {
        public RegisterResult()
        {
            UserId = string.Empty;
            Email = string.Empty;
            Message = "";
        }
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool Success => !string.IsNullOrEmpty(UserId);
        public string Message { get; set; }
    }
}
