using System.Threading.Tasks;
using Newtonsoft.Json;
using Tieno.MyPonto.Client.Service;

namespace Tieno.MyPonto.Client.Model
{
    public partial class Relationships: FetchableResource
    {
        [JsonProperty("account")]
        public AccountRelationship Account { get; set; }


        [JsonProperty("transactions")]
        public TransactionRelationships Transactions { get; set; }

        [JsonProperty("financialInstitution")]
        public FinancialInstitutionRelationships FinancialInstitution { get; set; }

        public Task<TransactionsResponse> GetTransactions()
        {
            return this._service.GetTransactions(Transactions.Links.Related);
        }
        


    }
}