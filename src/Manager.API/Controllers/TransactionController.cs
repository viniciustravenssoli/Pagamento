using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Manager.API.Utilities;
using Manager.API.ViewModels;
using Manager.Core.Exceptions;
using Manager.Services.DTO;
using Manager.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;

        public TransactionController(IMapper mapper, ITransactionService transactionService)
        {
            _mapper = mapper;
            _transactionService = transactionService;
        }

        [HttpPost]
        //[Authorize]
        [Route("/api/v1/transaction/create")]

        public async Task<IActionResult> Create([FromBody] CreateTransactionViewModel transactionViewModel)
        {
            try
            {
                var transactionDTO = _mapper.Map<TransactionDTO>(transactionViewModel);
                var transactionCreated = await _transactionService.Create(transactionDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Transação Efetuda com sucesso",
                    Success = true,
                    Data = transactionCreated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            // catch (Exception)
            // {
            //     return StatusCode(500, Responses.ApplicationErrorMessage());
            // }
        }

        [HttpGet]
        [Route("/api/v1/transaction/getUserTrasactionAsRecieverByID")]

        public async Task<IActionResult> GetAsReciever(long userId)
        {
            try
            {
                var user = await _transactionService.GetTransactionAsReceiver(userId);

                if (user == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Transação não encontrado",
                        Success = true,
                        Data = user
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Transação Encontrado com sucesso",
                    Success = true,
                    Data = user
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("/api/v1/transaction/getUserTrasactionAsSenderByID")]

        public async Task<IActionResult> GetAsSender(long userId)
        {
            try
            {
                var user = await _transactionService.GetTransactionAsSender(userId);

                if (user == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Transação não encontrado",
                        Success = true,
                        Data = user
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Transação Encontrado com sucesso",
                    Success = true,
                    Data = user
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("/api/v1/transaction/getUserTrasactionAsSenderByID/filterDate")]

        public async Task<IActionResult> GetAsSenderFilterDate(long userId, int mes)
        {
            try
            {
                var user = await _transactionService.GetTransactionAsSenderFilterDate(userId, mes);

                if (user == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Transaçao não encontrado",
                        Success = true,
                        Data = user
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Transaçao Encontrada com sucesso",
                    Success = true,
                    Data = user
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
    }
}