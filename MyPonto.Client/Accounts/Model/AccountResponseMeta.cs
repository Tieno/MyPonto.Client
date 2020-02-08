using Newtonsoft.Json;
using Tieno.MyPonto.Client.Model;

namespace Tieno.MyPonto.Client.Accounts.Model
{
    public partial class AccountResponseMeta
    {
        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }
}