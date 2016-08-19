using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;


namespace MRC.Framework.Mvc.Data
{
    public class WebService
    {
        string baseURL = string.Empty;
        private  HttpClient CreateHttpClientDBManager()
        {
            
            /*if (GlobalMvc.Host.ToLower().Contains("localhost")) baseURL = System.Configuration.ConfigurationManager.AppSettings["LocalDBManagerWebAPI"];
            else if (GlobalMvc.Host.ToLower().Contains("192.168.15.38")) baseURL = System.Configuration.ConfigurationManager.AppSettings["TestDBManagerWebAPI"];
            else baseURL = System.Configuration.ConfigurationManager.AppSettings["DBManagerWebAPI"];*/
            baseURL = System.Configuration.ConfigurationManager.AppSettings["DBManagerWebAPI"];
            HttpClient webClient = new HttpClient();
            webClient.BaseAddress = new Uri(baseURL);
            webClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            webClient.Timeout = new TimeSpan(0, 1, 30);
            return webClient;
        }


        public  T GetAPIServer<T>(string url)
        {

            HttpClient client = CreateHttpClientDBManager();
            HttpResponseMessage response = client.GetAsync(baseURL + url).Result;

            if (!response.IsSuccessStatusCode)
                throw new Exception(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));

            // Parse the response body. Blocking!
            T Data = response.Content.ReadAsAsync<T>().Result;
            return Data;
        }

        public T GetPostAPIServer<T>(string url, object Model)
        {
            HttpResponseMessage response;

            HttpClient client = CreateHttpClientDBManager();

            
            response = client.PostAsJsonAsync(baseURL + url, Model).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).Result;


           
            if (!response.IsSuccessStatusCode)
                throw new Exception(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));

            // Parse the response body. Blocking!
            return response.Content.ReadAsAsync<T>().Result;

        }
    }
}
