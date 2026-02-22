using InventoryManagmentSystem.Core.BackgroundJobs.Interfaces;
using InventoryManagmentSystem.Core.Data;
using InventoryManagmentSystem.Core.Interfaces;
using InventoryManagmentSystem.Core.Models;

namespace InventoryManagmentSystem.Core.BackgroundJobs.Concrete
{
    public class TransactionNotifier : ITransactionNotifier
    {
        private readonly IGenericRepository<InventoryTransaction> transactionRepo;

        public TransactionNotifier(IGenericRepository<InventoryTransaction> transactionRepo)
        {
            this.transactionRepo = transactionRepo;
        }
        public async Task ArchieveTranssactionThanYear()
        {
            var transactions = transactionRepo
                            .GetAll()
                            .Where(t=>t.CreatedAt <= DateTime.Today.AddYears(-1)&&
                                   t.IsArchived==false);

            foreach (InventoryTransaction transaction in transactions)
            {
                transaction.IsArchived = true;
                transactionRepo.UpdateByEntity(transaction);
            }

            await transactionRepo.SaveChangesAsync();
        }
        public void aa()
        {
            Console.WriteLine("dada");
        }
    }
}
