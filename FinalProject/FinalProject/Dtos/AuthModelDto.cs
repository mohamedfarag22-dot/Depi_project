﻿namespace FinalProject.Dtos
{
    public class AuthModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }

        public string UserId { get; set; }
        public DateTime ExpiresOn { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public string Message { get; set; }
    }
}