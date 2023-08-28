using System;
using System.Text.Json.Serialization;
using Manager.Domain.Entities;

namespace Manager.Services.DTO
{
    public class UserDTO
    {
        public String FirstName { get; set; } = string.Empty;
        public String LastName { get; set; } = string.Empty;
        public String Cpf { get; set; } = string.Empty;
        public String Email { get; set; } = string.Empty;
        public String Password { get; set; } = string.Empty;
        public decimal? Balance { get; set; }
        public UserType UserType { get; set; }
    }
}