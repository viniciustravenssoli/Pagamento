using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Domain.Entities;
using Manager.Services.DTO;

namespace Manager.Services.Interface
{
    public interface ITransactionService
    {
        Task<List<TransactionDTO>> GetTransactionAsReceiver(long id);
        Task<List<TransactionDTO>> GetTransactionAsSender(long userId);
        Task<TransactionDTO> Create(TransactionDTO transactionDTO);
        Task<List<TransactionDTO>> GetTransactionAsSenderFilterDate(long id, int mes);
    }
}