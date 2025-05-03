using InventoryClassLibrary.BackgroundJobs.Interfaces;
using InventoryClassLibrary.Data;
using InventoryClassLibrary.Interfaces;
using InventoryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryClassLibrary.BackgroundJobs.Concrete
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
