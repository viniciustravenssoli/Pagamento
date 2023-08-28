using System;
using System.ComponentModel.DataAnnotations;
using Manager.Domain.Entities;

namespace Manager.API.ViewModels{
    public class CreateUserViewModel
    {
        public String FirstName { get; set; } = string.Empty;
        public String LastName { get; set; } = string.Empty;
        public String Cpf { get; set; } = string.Empty;
        public String Email { get; set; } = string.Empty;
        public String Password { get; set; } = string.Empty;
        public UserType UserType { get; set; }
    }
}