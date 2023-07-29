using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Reflection.Metadata;
using Skyblock.Logger;

namespace Saky.main
{
    internal class GetPost
    {

        public static Task<string> Kkb()
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://api.iworld.best");
            HttpResponseMessage response = client.SendAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                
                return Task.FromResult(content.ToString());
            }
            else
            {
            
                return Task.FromResult(response.StatusCode.ToString());
            }
        }
    }

    }


