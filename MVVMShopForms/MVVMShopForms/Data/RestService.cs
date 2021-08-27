using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MVVMShopForms.Data
{
    public class RestService
    {
        HttpClient _client;
        Uri _UrlBase;

        public RestService(string urlBase)
        {
            _UrlBase = new Uri(urlBase);
            _client = new HttpClient();
            _client.BaseAddress = _UrlBase;
        }

        public RestService(string urlBase, string token) : this(urlBase)
            => _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);



        /// <summary>
        /// Obtiene una lista de T a partir de una url
        /// </summary>
        /// <typeparam name="T">Tipo de objeto deseado para la lista</typeparam>
        /// <param name="url">url del servicio</param>
        /// <returns>Lista de "T"</returns>
        public async Task<List<T>> GetDataAsync<T>(string url) 
        {
            List<T> TData = null;
            try
            {
                var response = await _client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    TData = JsonConvert.DeserializeObject<List<T>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error {ex.Message}");
            }

            return TData;
        }


        public async Task<string> PostAsync<T>(T data, string url)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());

            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task PutDataAsync<T>(T data, string url, int id) 
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = 
                await _client.PutAsync(string.Concat(url,"/",id.ToString()), content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());

            }
        }

        public async Task DeleteAsync(string url, int id) 
        {
            var response = await _client.DeleteAsync(String.Concat(url, "/", id.ToString()));
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());

            }
        }

       
    }
}
