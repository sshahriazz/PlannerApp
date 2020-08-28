using System;
using System.Collections.Generic;

namespace PlannerApp.Shared.Models
{
    public class UserManegerResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
        public Dictionary<string, string> UserInfo { get; set; }

    }
}
