using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace ChronosTests.Helpers {
    public static class HttpClientExtensions {
        public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string url,
            IDictionary<string, string> queryParams) {
            var queryString = QueryHelpers.AddQueryString(url, queryParams);
            return client.GetAsync(queryString);
        }

        public static Task<HttpResponseMessage> PatchAsJsonAsync(this HttpClient client, string requestUri, object o) {
            var json = JsonConvert.SerializeObject(o);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return client.PatchAsync(requestUri, content);
        }
    }
}