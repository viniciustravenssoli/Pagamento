using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Domain.Entities
{
    public class Transaction : Base
    {

        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;


        public long SenderId { get; set; }
        public virtual User Sender { get; set; }


        public long ReceiverId { get; set; }
        public virtual User Receiver { get; set; }

        public void CalcularNovoSaldo(User sender, User receiver, decimal amount){
            sender.Balance -= amount;
            receiver.Balance += amount;
        }
    }
}