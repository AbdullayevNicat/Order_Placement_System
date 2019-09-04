using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderSystem.Infrastructure.Service
{
    class ServiceProvider : AncestorServiceProvider
    {
        public static async Task<T> GetDataAsync<T>(string url)
            where T:class,new()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponse = await httpClient
                                                                .GetAsync(url);

                    string response = await httpResponse.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeAnonymousType(response, new T());
                }
            }
            catch (Exception exp)
            {
                //log
                throw;
            }
        }

        public static async Task<T> PutDataAsync<T>(object data, string url)
            where T : class, new()
        {
            try
            {
                string json = JsonConvert.SerializeObject(data);

                using (HttpContent httpContent = new StringContent(json))
                {

                    httpContent.Headers.ContentType = new System.Net
                            .Http.Headers.MediaTypeHeaderValue("application/json");

                    using (HttpClient httpClient = new HttpClient())
                    {
                        HttpResponseMessage httpResponse = await httpClient
                                                                    .PutAsync(url, httpContent);

                        string response = await httpResponse.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeAnonymousType(response, new T());
                    }
                }
            }
            catch (Exception exp)
            {
                //log
                throw;
            }
        }

        public static async Task<T> DeleteDataAsync<T>(string url)
            where T : class, new()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponse = await httpClient
                                                                .DeleteAsync(url);

                    string response = await httpResponse.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeAnonymousType(response, new T());
                }
            }
            catch (Exception exp)
            {
                //log
                throw;
            }
        }

        public static async Task<T> PostDataAsync<T>(object data, string url)
            where T : class, new()
        {
            try
            {
                return JsonConvert.DeserializeAnonymousType
                    (await PostDataAsync(JsonConvert.SerializeObject(data), url), new T());
            }
            catch (Exception exp)
            {
                //log
                throw;
            }
        }
    }
}
