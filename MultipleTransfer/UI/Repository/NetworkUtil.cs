using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using MultipleTransfer.Activities;
using MultipleTransfer.UI.Models;

namespace MultipleTransfer.UI.Repository
{

    

    public class NetworkUtil
    {


        private static HttpClientHandler insecureHandler;
        private static HttpClient client;
        private static NetworkUtil network;



        public static NetworkUtil networkUtil() {
             if(network == null) {
                network = new NetworkUtil();
                insecureHandler = GetInsecureHandler();
                client = new HttpClient(insecureHandler);
            }
          return network;
        }


        public  async Task<string> callSecurePost() {
            string uri = "https://localhost:5001/Customers/login";
            try
            {
                HttpResponseMessage message = await client.PostAsJsonAsync(uri, new LoginModel() { email = "Vic@gmail.com", password = "123" }); ;
                var something = await message.Content.ReadAsStringAsync();
                Console.WriteLine(something);
            }

            catch (Exception e) {
                string msg = e.Message;
               
            }


            return "";
        }

        public static HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
        //

        //https://localhost:5001/swagger/index.html
        internal static string baseUrl = "https://xmtapi.azurewebsites.net/customers/";
        internal static async Task<string> PostUSSDAsyc(string actionName, string mrawData)
        {
            string mresult = "";
            try
            {
                using (var mclient = new HttpClient() { BaseAddress = new Uri(baseUrl) })
                {
                    mclient.Timeout = TimeSpan.FromMinutes(1);
                    var response = await mclient.PostAsync($"{actionName}/", new StringContent(mrawData, Encoding.UTF8, "application/json")).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                  
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                mresult = "";
            }
            return mresult;
        }

        internal static async Task<string> PostUSSDAsycT(string actionName, string mrawData)
        {
            string mresult = "";
            try
            {
                using (var mclient = new HttpClient() { BaseAddress = new Uri(baseUrlGet) })
                {
                    mclient.Timeout = TimeSpan.FromMinutes(1);
                    var response = await mclient.PostAsync($"{actionName}/", new StringContent(mrawData, Encoding.UTF8, "application/json")).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }

                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                mresult = "";
            }
            return mresult;
        }


        internal static string baseUrlGet = "https://xmtapi.azurewebsites.net/";


        internal static async Task<string> GetAsycData(string actionName, string mrawData)
        {
            string mresult = "";
            try
            {
                using (var mclient = new HttpClient() { BaseAddress = new Uri(baseUrlGet) , })
                {
                    mclient.Timeout = TimeSpan.FromMinutes(1);
                    var response = await mclient.GetAsync($"{actionName}/").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                    else
                    {
                        return response.ReasonPhrase;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                mresult = "";
            }
            return mresult;
        }



        internal static async Task<string> GetAsycDataTrans(string actionName, string mrawData)
        {
            string mresult = "";
            try
            {
                using (var mclient = new HttpClient() { BaseAddress = new Uri(baseUrlGet), })
                {
                    mclient.Timeout = TimeSpan.FromMinutes(1);
                    var response = await mclient.GetAsync($"{actionName}/{mrawData}").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                    else
                    {
                        return response.ReasonPhrase;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                mresult = "";
            }
            return mresult;
        }

        //public static HttpClientHandler GetInsecureHandler()
        //{
        //    HttpClientHandler handler = new HttpClientHandler();
        //    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        //    {
        //        if (cert.Issuer.Equals("CN=localhost"))
        //            return true;
        //        return errors == System.Net.Security.SslPolicyErrors.None;
        //    };
        //    return handler;
        //}


    }
}
