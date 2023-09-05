﻿namespace Service.Dtos.User
{
    public class UserWithTokenDto
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
