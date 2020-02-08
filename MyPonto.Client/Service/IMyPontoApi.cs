using Tieno.MyPonto.Client.Accounts;
using Tieno.MyPonto.Client.Synchronization;
using Tieno.MyPonto.Client.Transactions;

namespace Tieno.MyPonto.Client.Service
{
    public interface IMyPontoApi
    {
        
        IAccountApi Accounts { get; set; }

        ITransactionApi Transactions { get; set; }
       
        ISynchronizationApi Synchronizations { get; set; }
    }
}