﻿namespace FribergHomeAPI.DTOs
{
    public class AuthResponse
    {
        public string Email { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public int AgentId { get; set; }
    }
}
