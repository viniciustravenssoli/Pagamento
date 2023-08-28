using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Manager.Domain.Entities
{
    [Index(nameof(Cpf), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    [MetadataType(typeof(User))]
    public class User : Base
    {
        public User(UserType userType)
        {
            TransactionAsSender = new HashSet<Transaction>();
            TransactionAsReciever = new HashSet<Transaction>();
            UserType = userType;
        }

        public String FirstName { get; set; } = string.Empty;
        public String LastName { get; set; } = string.Empty;
        public String Cpf { get; set; } = string.Empty;
        public String Email { get; set; } = string.Empty;
        public String Password { get; set; } = string.Empty;
        public decimal Balance { get; set; }

        [EnumDataType(typeof(UserType))]
        public UserType UserType { get; set; }

        public virtual ICollection<Transaction> TransactionAsSender { get; set; }

        public virtual ICollection<Transaction> TransactionAsReciever { get; set; }

    }
}
