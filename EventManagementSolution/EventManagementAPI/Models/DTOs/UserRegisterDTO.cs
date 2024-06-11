﻿namespace EventManagementAPI.Models.DTOs
{
    public class UserRegisterDTO 
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? UserType { get; set; }
        public string Password { get; set; }
    }
}
