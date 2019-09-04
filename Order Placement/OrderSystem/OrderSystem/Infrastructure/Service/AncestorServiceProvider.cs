using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderSystem.Infrastructure.Service
{
    abstract class AncestorServiceProvider
    {
        protected static async Task<string> PostDataAsync(string json, string url)
        {
            using (HttpContent httpContent = new StringContent(json))
            {
                httpContent.Headers.ContentType = new System.Net
                            .Http.Headers.MediaTypeHeaderValue("application/json");
               
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponse = await httpClient.PostAsync(url, httpContent);

                    string result = await httpResponse.Content.ReadAsStringAsync();

                    return result;
                }
            }
        }
    }
}
