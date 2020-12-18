using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;

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