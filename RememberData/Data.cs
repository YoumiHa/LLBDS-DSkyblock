using Newtonsoft.Json.Linq;
using Skyblock.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.RememberData
{
    public class Data
    {
        public static JObject c;
        public List<Newtonsoft.Json.Linq.JToken> x = new List<JToken>();
        public static JObject y;
        internal  Task<string> ServerStarted()
        {
            try
            {
                var cp = File.ReadAllText("plugins/SkyBlock/Share.json");
                var scd = JObject.Parse(File.ReadAllText("plugins/SkyBlock/config.json").ToString());
              c= JObject.Parse(cp);
              x= scd["landOP"].ToList();
                y = JObject.Parse(scd["mainland"].ToString());
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return Task.FromResult("1");
        }
        public static Task ServerStopped1() {

            try
            {
                File.WriteAllText("plugins/SkyBlock/Share.json",c.ToString());
                Data d = new();
                d.ServerStarted();
                
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
