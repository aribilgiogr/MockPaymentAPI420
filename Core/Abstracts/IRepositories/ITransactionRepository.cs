using Core.Concretes.Entities;

namespace Core.Abstracts.IRepositories
{
    public interface ITransactionRepository
    {
        Task<Transaction?> GetByIdAsync(Guid id);
        Task<Transaction?> GetByOrderIdAsync(string orderId);
        Task CreateAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
    }
}
