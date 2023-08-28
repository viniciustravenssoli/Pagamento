using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.API.ViewModels
{
    public class CreateTransactionViewModel
    {
        public decimal Amount { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
    }
}