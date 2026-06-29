using Core.Abstracts.IRepositories;
using Core.Concretes.Entities;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PaymentDbContext context;

        public TransactionRepository(PaymentDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Transaction transaction) => await context.Transactions.AddAsync(transaction);

        public async Task<Transaction?> GetByIdAsync(Guid id) => await context.Transactions.FindAsync(id);

        public async Task<Transaction?> GetByOrderIdAsync(string orderId) => await context.Transactions.FirstOrDefaultAsync(x => x.OrderId == orderId);

        public async Task UpdateAsync(Transaction transaction) => await Task.Run(() => context.Transactions.Update(transaction));
    }
}
