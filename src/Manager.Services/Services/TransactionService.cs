using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.DTO;
using Manager.Services.Interface;
using Newtonsoft.Json;

namespace Manager.Services.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly HttpClient _httpClient;

        public TransactionService(IMapper mapper, ITransactionRepository transactionRepository, IUserService userService, HttpClient httpClient, IUserRepository userRepository)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
            _userService = userService;
            _httpClient = httpClient;
            _userRepository = userRepository;
        }

        public async Task<TransactionDTO> Create(TransactionDTO transactionDTO)
        {
            var isAuth = await this.AuthorizeTransaction();

            if (!isAuth)
            {
                throw new DomainException("Nao Autorizado");
            }

            var sender = await _userRepository.Get(transactionDTO.SenderId);
            
            var IsValid = await this.ValidateTransaction(sender, transactionDTO.Amount);
            
            if(!IsValid)
            {
                throw new DomainException("Usuario não possui saldo suficiente");
            }

            var receiver = await _userRepository.Get(transactionDTO.ReceiverId);

            if (sender.UserType == UserType.Merchant)
            {
                throw new DomainException("Usuarios do tipo Merchant nao podem realizar transaçoes");
            }

            var transaction = _mapper.Map<Transaction>(transactionDTO);

            transaction.CalcularNovoSaldo(sender, receiver, transaction.Amount);

            var transactionCreated = await _transactionRepository.Create(transaction);

            await _userRepository.Update(sender);
            await _userRepository.Update(receiver);

            return _mapper.Map<TransactionDTO>(transactionCreated);
        }

        public async Task<List<TransactionDTO>> GetTransactionAsReceiver(long userId)
        {
            var transactions = await _transactionRepository.GetTransactionAsReceiver(userId);

            return _mapper.Map<List<TransactionDTO>>(transactions);
        }

        public async Task<List<TransactionDTO>> GetTransactionAsSender(long userId)
        {
            var transactions = await _transactionRepository.GetTransactionAsSender(userId);

            return _mapper.Map<List<TransactionDTO>>(transactions);
        }

        public async Task<List<TransactionDTO>> GetTransactionAsSenderFilterDate(long id, int mes)
        {
            var transactions = await _transactionRepository.GetTransactionAsSenderFilterDate(id, mes);

            return _mapper.Map<List<TransactionDTO>>(transactions);
        }

        public async Task<bool> AuthorizeTransaction()
        {
            var apiUrl = "https://run.mocky.io/v3/8fafdd68-a090-496f-8c9a-3442cf30dae6";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();

                try
                {
                    AuthorizationResponse apiResponse = JsonConvert.DeserializeObject<AuthorizationResponse>(jsonResponse);
                    return apiResponse.Message == "Autorizado";
                }
                catch (JsonException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return false;
        }

        public Task<bool> ValidateTransaction(User sender, decimal amount)
        {
            if (sender.Balance >= amount)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }


        

        // using (HttpClient client = new HttpClient())
        // {
        //     string apiUrl = "https://run.mocky.io/v3/8fafdd68-a090-496f-8c9a-3442cf30dae6";
        //     HttpResponseMessage response = await client.GetAsync(apiUrl);

        //     if (response.IsSuccessStatusCode)
        //     {
        //         string jsonResponse = await response.Content.ReadAsStringAsync();

        //         try
        //         {
        //             using (JsonDocument document = JsonDocument.Parse(jsonResponse))
        //             {
        //                 JsonElement root = document.RootElement;
        //                 if (root.TryGetProperty("message", out JsonElement messageElement))
        //                 {
        //                     return messageElement.GetString() == "Autorizado";
        //                 }
        //             }
        //         }
        //         catch (JsonException)
        //         {
        //             return false;
        //         }
        //     }


        //     return false;
        // }
    }
}


