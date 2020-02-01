using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyPonto.Client.Service
{
    public static class HttpClientExtensions
    {
        public static async Task<T> GetAs<T>(this HttpClient thisClient, string uri)
        {
            var response = await thisClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
        public static async Task<T> GetAs<T>(this HttpClient thisClient, Uri uri)
        {
            var response = await thisClient.GetAsync(uri); 
            return await response.GetAs<T>();
        }
        public static async Task<T> GetAs<T>(this HttpResponseMessage thisResponse)
        {
            
            var content = await thisResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}