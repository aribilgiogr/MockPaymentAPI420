using Core.Abstracts;
using Core.Abstracts.IRepositories;
using Core.Abstracts.IServices;
using Core.Concretes.Entities;
using Core.Concretes.Enums;
using Core.Concretes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            this.transactionRepository = transactionRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Reply<Transaction>> ProcessPaymentAsync(Transaction transaction)
        {
            transaction.Type = TransactionType.Payment;
            transaction.Status = TransactionStatus.Success;

            await transactionRepository.CreateAsync(transaction);
            await unitOfWork.CommitAsync();

            return new Reply<Transaction>
            {
                Success = true,
                Message = "Ödeme işlemi başarıyla tamamlandı.",
                Data = transaction
            };
        }



        public async Task<Reply<Transaction>> ProcessRefundAsync(string orderId, decimal amount)
        {
            var transaction = await transactionRepository.GetByOrderIdAsync(orderId);
            if (transaction == null)
            {
                return new Reply<Transaction>
                {
                    Success = false,
                    Message = "İade edilmek istenen sipariş bulunamadı",
                    Data = null
                };
            }

            var refundTransaction = new Transaction
            {
                OrderId = orderId,
                Amount = amount,
                CardHolderName = transaction.CardHolderName,
                MaskedCardNumber = transaction.MaskedCardNumber,
                Type = TransactionType.Refund,
                Status = TransactionStatus.Success
            };

            await transactionRepository.CreateAsync(refundTransaction);
            await unitOfWork.CommitAsync();

            return new Reply<Transaction>
            {
                Success = true,
                Message = "İade işlemi başarıyla tamamlandı.",
                Data = refundTransaction
            };
        }
    }
}
