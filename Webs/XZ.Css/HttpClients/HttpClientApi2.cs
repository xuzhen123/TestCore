using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace XZ.Css.HttpClients
{
    public class HttpClientApi2
    {
        HttpClient _httpClient;
        public HttpClientApi2(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<string> GetResponseMessage(string url)
        {
            return await _httpClient.GetStringAsync(url);
        }
    }
}
