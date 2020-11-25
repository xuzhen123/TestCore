using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XZ.Css.HttpClients
{
    public class HttpClientApi1
    {
        HttpClient _httpClient;
        public HttpClientApi1(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", @"Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)");
            _httpClient.DefaultRequestHeaders.Add("Accept", @"text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
        }

        public async Task<string> GetAsync(string url, Dictionary<string, string> dictionary)
        {
            string urlNew = $"{url}/{BuildParam(dictionary)}";

            return await _httpClient.GetStringAsync(urlNew);
        }

        public async Task<T> PostAsync<T>(string url, Dictionary<string, string> dictionary, string contentType = "application/x-www-form-urlencoded")
        {
            using (Stream stream = new MemoryStream(Encoding.ASCII.GetBytes(BuildParam(dictionary))))
            {
                using (HttpContent content = new StreamContent(stream))
                {
                    content.Headers.Add("Content-Type", contentType);

                    var response = await _httpClient.PostAsync(url, content);

                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
            }
        }

        private string BuildParam(Dictionary<string, string> dictionary)
        {
            StringBuilder builder = new StringBuilder();

            if (dictionary != null && dictionary.Count > 0)
            {
                foreach (var item in dictionary)
                {
                    builder.Append($"{item.Key}={item.Value}&");
                }
            }
            return builder.ToString().Substring(0, builder.Length - 1);
        }
    }
}
