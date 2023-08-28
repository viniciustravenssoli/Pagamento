using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Services.DTO
{
    public class TransactionDTO
    {
        public decimal Amount { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
    }
}