using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tieno.MyPonto.Client.Service;

namespace Tieno.MyPonto.Client.Model
{
    public partial class TransactionsResponseLinks: FetchableResource
    {
        [JsonProperty("prev")]
        public Uri Prev { get; set; }

        [JsonProperty("next")]
        public Uri Next { get; set; }

        [JsonProperty("first")]
        public Uri First { get; set; }

        public Task<TransactionsResponse> GetNextPage()
        {
            return this._service.GetTransactions(Next);
        }
        public Task<TransactionsResponse> GetPreviousPage()
        {
            return this._service.GetTransactions(Prev);
        }
        public Task<TransactionsResponse> GetFirstPage()
        {
            return this._service.GetTransactions(First);
        }
    }
}