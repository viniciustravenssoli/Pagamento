using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Manager.Infra.Repositiries;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        private readonly ManagerContext _context;
        public TransactionRepository(ManagerContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Transaction>> GetTransactionAsReceiver(long id)
        {
            var transaction = _context.Transactions
                                .Include(x => x.Receiver)
                                .Where(x => x.ReceiverId == id)
                                .AsNoTracking()
                                .ToListAsync();
            
            return transaction;
        }

        public Task<List<Transaction>> GetTransactionAsSender(long id)
        {
            var transaction = _context.Transactions
                                .Include(x => x.Sender)
                                .Where(x => x.SenderId == id)
                                .AsNoTracking()
                                .ToListAsync();
            
            return transaction;
        }

        public Task<List<Transaction>> GetTransactionAsSenderFilterDate(long id, int mes)
        {
            var transaction = _context.Transactions
                                .Include(x => x.Sender)
                                .Where(x => x.SenderId == id &&
                                            x.TransactionDate.Month == mes)
                                .AsNoTracking()
                                .ToListAsync();
                        
                               
            
            return transaction;
        }
    }
}