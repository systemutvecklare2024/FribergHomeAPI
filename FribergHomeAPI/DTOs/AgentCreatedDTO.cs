﻿using FribergHomeAPI.Models;

namespace FribergHomeAPI.DTOs
{
    public class AgentCreatedDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int AgencyId { get; set; }
    }
}
