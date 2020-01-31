using System.Threading.Tasks;
using MyPonto.Client.Service;
using Newtonsoft.Json;

namespace MyPonto.Client.Model
{
    public partial class TransactionRelationships 
    {
        [JsonProperty("links")]
        public TransactionsLinks Links { get; set; }

        
    }
}
