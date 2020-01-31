using System.Threading.Tasks;
using MyPonto.Client.Service;
using Newtonsoft.Json;

namespace MyPonto.Client.Model
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