using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Domain.Entities;

namespace Manager.Infra.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<List<Transaction>> GetTransactionAsReceiver(long id);

        Task<List<Transaction>> GetTransactionAsSender(long id);

        Task<List<Transaction>> GetTransactionAsSenderFilterDate(long id, int mes);
    }
}