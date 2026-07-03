using Core.Abstracts;
using Data.Contexts;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentDbContext context;

        public UnitOfWork(PaymentDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async ValueTask DisposeAsync() => await context.DisposeAsync();
    }
}
