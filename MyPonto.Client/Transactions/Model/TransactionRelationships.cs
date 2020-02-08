using Newtonsoft.Json;

namespace Tieno.MyPonto.Client.Transactions.Model
{
    public partial class TransactionRelationships 
    {
        [JsonProperty("links")]
        public TransactionsLinks Links { get; set; }

        
    }
}
