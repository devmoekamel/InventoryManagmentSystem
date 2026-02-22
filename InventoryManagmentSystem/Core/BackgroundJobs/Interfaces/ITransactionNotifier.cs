using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Core.BackgroundJobs.Interfaces
{
    public interface ITransactionNotifier
    {
        Task ArchieveTranssactionThanYear();
    }
}
