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
        private readonly INotificationService _notificationService;

        public TransactionService(IMapper mapper, ITransactionRepository transactionRepository, IUserService userService, HttpClient httpClient, IUserRepository userRepository, INotificationService notificationService)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
            _userService = userService;
            _httpClient = httpClient;
            _userRepository = userRepository;
            _notificationService = notificationService;
        }

        public async Task<TransactionDTO> Create(TransactionDTO transactionDTO)
        {
            var isAuth = await AuthorizeTransaction();
            if (!isAuth)
            {
                throw new DomainException("Nao Autorizado");
            }
            
            var sender = await _userRepository.Get(transactionDTO.SenderId);
            if (sender.UserType == UserType.Merchant)
            {
                throw new DomainException("Usuarios do tipo Merchant nao podem realizar transaçoes");
            }

            var IsValid = await ValidateTransaction(sender, transactionDTO.Amount);
            if (!IsValid)
            {
                throw new DomainException("Usuario não possui saldo suficiente");
            }

            var receiver = await _userRepository.Get(transactionDTO.ReceiverId);

            var transaction = _mapper.Map<Transaction>(transactionDTO);

            var transactionCreated = await _transactionRepository.Create(transaction);

            await ProcessTransaction(transaction, sender, receiver);

            return _mapper.Map<TransactionDTO>(transactionCreated);
        }

        private async Task ProcessTransaction(Transaction transaction, User sender, User receiver)
        {
            try
            {
                // Realizar lógica de processamento da transação
                transaction.CalcularNovoSaldo(sender, receiver, transaction.Amount);

                // Atualizar os saldos dos usuários
                await _userRepository.Update(sender);
                await _userRepository.Update(receiver);

                // Enviar notificação de transação
                string destinatario = "kenya.cruickshank42@ethereal.email";
                string corpo = $"Transação recebida no valor de ${transaction.Amount}";
                string assunto = "Transação";

                // Aqui, a ideia seria receber o email do reciever, porem como estou utilizando ethereal usei o email teste fornecido para poder realizar o processo.
                // tbm poderiamos criar um usuario com o email fornecido pelo ethreal e no lugar do {destinatario} passiariamos o {reciver.Email}.
                // tudo isso so ira funcionar ate o site ethereal desabilitar o email fornecido.
                _notificationService.EnviarEmail(/* receiver.Email  --> {destinatario}*/destinatario, assunto, corpo);
            }
            catch (Exception ex)
            {
                // Lidar com exceções, fazer log, ou reverter a transação, se necessário
                throw new DomainException("Erro ao processar a transação", ex);
            }
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


