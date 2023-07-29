using LiteLoader.Event;
using MC;
using Skyblock.Logger;
using Skyblock.FileV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using Skyblock.Common.Form;

namespace Skyblock.Stru
{
    internal class LoadStru
    {
     private  int x;
      private  int y;
     private   int z;
        private string d;
        private string posdata = "plugins/SkyBlock/pos.json";
        public void LoadStr(Player player, string jk)
        {
            LoadStr1(player,jk);
        }
        private void LoadStr1(Player player,string jk)
         {

           
            try
            {
               
                int sax = int.Parse(FileV.ReadFile.Get("x", posdata).Result.ToString());
                player.SendTextPacket("空岛加载中!", TextType.Tip);
                if (CheckLand(sax) == true)
                 {
                    int a = int.Parse(FileV.ReadFile.Get("x", posdata).Result.ToString());
                    int b = int.Parse(FileV.ReadFile.Get("y", posdata).Result.ToString());
                    int c = int.Parse(FileV.ReadFile.Get("z", posdata).Result.ToString());
                    int k = int.Parse(ReadFile.Get("LandSize", "plugins/SkyBlock/config.json").Result.ToString());
                    switch (jk)
                    {
                        case "1":
                              x = 10;
                              y = 15;
                              z = 16;
                             d = "经典";
                            Level.Runcmd($"structure load island1 {a} {b} {c}");
                            break;
                        case "2":
                            x = 4;
                            y = 5;
                            z = 3;
                            d = "双子";
                            Level.Runcmd($"structure load island2 {a} {b} {c}");
                            break;
                        case "3":
                            x = 0;
                            y = 2;
                            z = 1;
                            d = "极难";
                            Level.Runcmd($"structure load island3 {a} {b} {c}");
                            break;
                    }
                    logger.Info(a + k);
                    int f = a + k + k;
                    int lp = b;
                    FileV.ReadFile.Set("x", f, posdata);
                    FileV.ReadFile.Set("y", lp, posdata);
                    int ps = a + x;
                    int py = y + b;
                    int px = z + c;
                    logger.Info(ps);
                    logger.Info(py);
                 
                    try
                    {
                      var tttt =   backtoland.LANDapi(player.Xuid, new Vec3(a + k - 20, 300, c + k - 10),new Vec3(a - k + 10, -64,c - k + 20),0);
                        savadata($"plugins/SkyBlock/data/{player.Xuid}_{player.RealName}.json", player.RealName, player.Xuid,d, ps, py, px,tttt);
                        Level.Runcmd($"execute as '{player.RealName}' at '{player.RealName}' run spawnpoint '{player.RealName}' {ps} {py} {px}");
                        player.Teleport(new Vec3(ps, py, px), 0);
                       
                        player.Runcmd("gmrule island tp set");
                    }
                    catch (Exception ex)
                    {
                        logger.Info(ex.StackTrace);
                        player.SendText("存储错误!");
                        logger.Warn("存储错误!");
                    }
                   
                    try
                    {
                       
                    }
                    catch (Exception esc)
                    {
                        logger.Warn(esc);
                    }
                }
                else
                {
                    player.SendToastPacket("恭喜,服务器可创建领地的位置已经用尽!","恭喜");
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                player.SendText(e.Message);
                player.SendToastPacket("生成失败","错误码-1 请让腐竹报告给开发者!");
            }
           

           // Level.Runcmd($"execute as {player.RealName} at {player.RealName} run structure load island1 {x} {y} {z}");

        }
  public bool CheckLand(int x)
        {
            return CheckLand1(x);
        }
        private bool CheckLand1(int x)
        {
            int c = int.Parse(FileV.ReadFile.Get("z", posdata).Result.ToString());
            int k = int.Parse(ReadFile.Get("LandSize", "plugins/SkyBlock/config.json").Result.ToString());
            if (x >= 9600)
            {
                FileV.ReadFile.Set("x", -9600, posdata);
                return true;
            }
            
            else if(x>= -1000-k && x<-10)
            {
                FileV.ReadFile.Set("x", 1000+k+100, posdata);
                FileV.ReadFile.Set("z", c+k+k+2, posdata);
                return true;
            }else if(c >=10000)
            {
                FileV.ReadFile.Set("z", -10000, posdata);
                return true;
            }else if (c<= -k-k && c >= -k - k-k)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public void savadata(string k, string playername, string playerxuid, string zl,int x, int y, int z,string klp)
        {
            FileStream dsc = File.Create(k);
            dsc.Close();
            JObject keyValuePairs = new JObject();
            keyValuePairs.Add(playername, new JObject(
                new JProperty("zl", zl),
                new JProperty("x", x),
                new JProperty("y", y),
                new JProperty("z", z),
                new JProperty("landid", klp)
                )) ; 
            File.WriteAllText(k, keyValuePairs.ToString());
        }
    }
}
