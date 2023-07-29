using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Skyblock.Logger;
using Newtonsoft.Json.Linq;
using LiteLoader.Logger;
using System.Security.Cryptography.X509Certificates;
using Skyblock.Update.Get;
using LiteLoader.Event;
using MC;

namespace Skyblock.FileV
{
    internal class MoveFile
    {
        Getdata g = new Getdata();
        //检查是否有结构文件
        internal async void Check()
        {
      
          await  Task.Run(async () =>
            {
                if (!File.Exists("plugins/SkyBlock/Share.json"))
                {
                    FileStream c = File.Create("plugins/SkyBlock/Share.json");
                    c.Close();
                    JObject jk = new JObject();
                    File.WriteAllText("plugins/SkyBlock/Share.json", "{}");
                    logger.Info("新的配置文件" + jk);
                }
             
                if (!Directory.Exists("plugins/SkyBlock/MoreLand"))
                {
                    Directory.CreateDirectory("plugins/SkyBlock/MoreLand");
                    Directory.CreateDirectory("plugins/SkyBlock/lang");
                }
                if (!File.Exists("plugins/lib/Skyblockdata-save.lua"))
                {
                    await g.DownloadFileBigByUrl1("BaseLiblua.lua", "https://tickingu.lanzoub.com/ixHqi118kt8d", "i6wy", "plugins/lib");
                    await g.DownloadFileBigByUrl1("Skyblockdata-save.lua", "https://tickingu.lanzoub.com/iKmLE118sfgf", "cb0o", "plugins/lib");
               
                    await g.DownloadFileBigByUrl1("zh_CN.json", "https://tickingu.lanzoub.com/iyzSk118rdta", "5drs", "plugins/SkyBlock/lang");
                     Level.Runcmd("ll load plugins/lib/Skyblockdata-save.lua");
                }
                //检测是否有默认结构文件
                if (File.Exists("./behavior_packs/vanilla/structures/island1.mcstructure") == false ||  File.Exists("./behavior_packs/vanilla/structures/island2.mcstructure") ==false || File.Exists("./behavior_packs/vanilla/structures/island3.mcstructure") == false)
                {
                  
                    await g.DownloadFileBigByUrl1("island1.mcstructure", "https://wwiq.lanzoub.com/ibJVt10vnbob", "a22g", "behavior_packs/vanilla/structures");
                    await g.DownloadFileBigByUrl1("island2.mcstructure", "https://wwiq.lanzoub.com/irHBU10vovjc", "e7v3", "behavior_packs/vanilla/structures");
                    await g.DownloadFileBigByUrl1("island3.mcstructure", "https://wwiq.lanzoub.com/iUCKY10vovle", "4mzw", "behavior_packs/vanilla/structures");
                }
                //检测是否生成默认pos
                if (!File.Exists("plugins/SkyBlock/pos.json"))
                {
              FileStream  c =    File.Create("plugins/SkyBlock/pos.json");
                    c.Close();
                    File.WriteAllText("plugins/SkyBlock/pos.json","{}");
                    JObject jk = new JObject();
                    jk.Add(new JProperty("x",2000));
                    jk.Add(new JProperty("y", 100));
                    jk.Add(new JProperty("z", 2000));
                    File.WriteAllText("plugins/SkyBlock/pos.json",jk.ToString());
                   logger.Info("新的配置文件"+jk);
                 
                }
                //检测是否生成config
                if (!File.Exists("plugins/SkyBlock/config.json"))
                {
                    FileStream c = File.Create("plugins/SkyBlock/config.json");
                    c.Close();
                    File.WriteAllText("plugins/SkyBlock/config.json", "{}");
                    JObject keys = new JObject()
                    {
                      new JProperty("overland",true),
                      new JProperty("Nether",true)
                    };   
                    
                    JObject jk = new JObject();
                    string xs = "shop";
                    
                    jk.Add(new JProperty("normal_x", 0));
                    jk.Add(new JProperty("normal_z", 0));
                    jk.Add(new JProperty("portect_range", 400));
                    jk.Add(new JProperty("LandSize",400));
                    jk.Add(new JProperty("islandShop", false));
                    jk.Add(new JProperty("shopcomm", xs));
                    jk.Add(new JProperty("Creativeland",keys));
                    File.WriteAllText("plugins/SkyBlock/config.json",jk.ToString());

                    logger.Info("新的配置文件" + jk);
                   
                }//分享
                if (File.Exists("plugins/SkyBlock/config.json"))
                {
                    if (JObject.Parse(File.ReadAllText("plugins/SkyBlock/config.json"))["landOP"] == null)
                    {
                        List<string> x = new List<string>()
                        {
                            "114514",
                            "11451419180"
                        };

                        JObject keys = JObject.Parse(File.ReadAllText("plugins/SkyBlock/config.json"));
                        keys.Add(new JProperty("landOP",x));
                        File.WriteAllText("plugins/SkyBlock/config.json",keys.ToString());
                    }
                }
                if(File.Exists("plugins/SkyBlock/config.json"))
                {
                    if (JObject.Parse(File.ReadAllText("plugins/SkyBlock/config.json"))["moreLand"] == null)
                    {
                        JObject keys1 = new JObject()
                    {
                      new JProperty("Open",false),
                     
                    };
                        JObject keys = JObject.Parse(File.ReadAllText("plugins/SkyBlock/config.json"));
                        keys.Add(new JProperty("moreLand", keys1));
                        File.WriteAllText("plugins/SkyBlock/config.json",keys.ToString());
                        logger.Info("新的配置文件" + keys1);
                    }
                }
                if (File.Exists("plugins/SkyBlock/config.json"))
                {
                    if (JObject.Parse(File.ReadAllText("plugins/SkyBlock/config.json"))["mainland"] == null)
                    {
                        JObject keys1 = new JObject()
                    {
                      new JProperty("x","0"),
                      new JProperty("y","100"),
                      new JProperty("z","100")
                    };
                        JObject keys = JObject.Parse(File.ReadAllText("plugins/SkyBlock/config.json"));
                        keys.Add(new JProperty("mainland", keys1));
                        File.WriteAllText("plugins/SkyBlock/config.json", keys.ToString());
                        logger.Info("新的配置文件" + keys1);
                    }
                }
                

            });
        }

       
    }
}
