using MC;
using Newtonsoft.Json.Linq;
using Skyblock.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.FileV
{
    internal class ReadFile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">读取的名称</param>
        /// <param name="y">读取的路径</param>
        /// <returns></returns>
        public static Task<object> Get(string x,string y)
        {
            JObject cp = new JObject();
            try
            {
                var c = File.ReadAllText(y);
                try
                {
                cp = JObject.Parse(c);
                }catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return Task.FromResult<object>(true) ;
                }
            }
            catch (Exception ex)
            {
              logger.Error(ex.Message);
                return Task.FromResult<object>(false);
            }
           
            return Task.FromResult<object>(cp[x]);
        }
        public static Task<bool> Set<T>(string x,T z,string y)
        {
           JObject keyValuePairs = new JObject();
            try
            {
                keyValuePairs = JObject.Parse(File.ReadAllText(y));
           
                    keyValuePairs.Remove(x);
               
                 
                
                keyValuePairs.Add(new JProperty(x, z));
                try
                {
                    File.WriteAllText(y,keyValuePairs.ToString());
                }
                catch (Exception exa)
                {
                    logger.Error(exa.Message);
                    return Task.FromResult(false);
                }
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Task.FromResult(false);
            }
        }
    }
}
