using Core.Concretes.Entities;
using Core.Concretes.Models;

namespace Core.Abstracts.IServices
{
    public interface IPaymentService
    {
        Task<Reply<Transaction>> ProcessPaymentAsync(Transaction transaction);
        Task<Reply<Transaction>> ProcessRefundAsync(string orderId, decimal amount);
    }
}
