using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Utilities
{
    public class InitielizeHttpClient
    {
        public HttpClient apiClient;

        public void InitielizeClient ()
        {

            string api = ConfigurationManager.AppSettings["api"];

            apiClient = new HttpClient ();
            apiClient.BaseAddress = new Uri ( api);
            apiClient.DefaultRequestHeaders.Accept.Clear();
                apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue ("application/json"));
        
        }
    
    }
}
