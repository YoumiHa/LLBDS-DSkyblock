using LiteLoader.NET;
using LiteLoader.Logger;
using Skyblock.Update.Get;
using LiteLoader;
using Skyblock.Logger;
using System.Diagnostics;
using Skyblock.FileV;
using Skyblock.Common;
using Skyblock.RememberData;

using Newtonsoft.Json.Linq;
using MC;
using LiteLoader.Event;

namespace Skyblock;
[PluginMain("Skyblock")]
public class Skyblock : IPluginInitializer
{
    MoveFile m = new();
    RegCommon r = new RegCommon();
    Data j = new Data();
    Getdata g = new Getdata();
    public Dictionary<string, string> MetaData => new()
    {
        {"Auther","OSMIUM"}
    };

    public string Introduction => "Nothing";

    public System.Version Version => new(2, 0, 0);

   private async void OnShutdown()
    {

    m.Check();
        r.RegCommo();
        await j.ServerStarted();
        ServerStartedEvent.Subscribe_Ref((a) =>
        {
            Level.Runcmd("ll load plugins/lib/Skyblockdata-save.lua");
            return true;
        });
    }

    public async void OnInitialize()
    {
     
        Console.WriteLine(@"         _____  __ ____  __ ____   __    ____   ______ __ __
        / ___/ / //_/\ \/ // __ ) / /   / __ \ / ____// //_/
        \__ \ / ,<    \  // __  |/ /   / / / // /    / ,<   
       ___/ // /| |   / // /_/ // /___/ /_/ // /___ / /| |  
      /____//_/ |_|  /_//_____//_____/\____/ \____//_/ |_| 

      -----------------------SkyBlock-----------------------
" +
 "    \r");
        Console.WriteLine("                                ");
        if (!Directory.Exists("plugins/SkyBlock") || !Directory.Exists("plugins/SkyBlock/structures"))
        {
            Directory.CreateDirectory("plugins/SkyBlock");
            logger.Warn("主文件夹已创建");
            Directory.CreateDirectory("plugins/SkyBlock/data");
            logger.Warn("data文件夹已创建");
            Directory.CreateDirectory("plugins/SkyBlock/structures");
            logger.Warn("结构文件夹已创建");
            elua();
          
        }
        else if (!File.Exists("plugins/SkyBlock/eula.gxh"))
        {
           elua();
        }
        else if (File.Exists("plugins/SkyBlock/eula.gxh"))
        {
        
            OnShutdown();
            ask();

        }
        else
        {
            logger.Error("插件初始化错误");
        }

    }
    JObject io2 = new JObject();
    JObject io3 = new JObject();
    string version = "1.2.2";
     private async void elua()
    {
        string x;
        try
        {
            var io = await Saky.main.GetPost.Kkb();
            io2 = JObject.Parse(io);
            io3 = JObject.Parse(io2["data"].ToString());

        }
        catch
        {
            logger.Error("请联网!");
        }
        Console.Write(io3["notice"].ToString()+"\n");
        x = Console.ReadLine();
        switch (x)
        {
            case "y":
                File.Create("plugins/SkyBlock/eula.gxh");
                OnShutdown();
                ask();
                break;
            default:
                logger.Warn("您没有同意协议,插件将不会加载!");
                break;
        }
        Console.WriteLine("\n ");

       
    }
    private async void ask()
    {
        try
        {
            var io = await Saky.main.GetPost.Kkb();
            
            io2 = JObject.Parse(io);
            io3 = JObject.Parse(io2["data"].ToString());
            if (io2["version"].ToString() == version)
            {

            }
            else
            {
                string hl;
                Console.Write("您要更新吗?,输入Y更新,输入N不更新");
                hl = Console.ReadLine();
                if (hl == "Y")
                {
                    logger.Warn(io2 + "\n");
                    logger.Warn("发现新版本");
                    logger.Warn($"版本号: {io2["version"].ToString()}");
                    logger.Warn($"大小: {io3["file"].ToString()}");
                    logger.Warn($"更新内容: {io3["newmsg"].ToString()}");
                    logger.Warn($"构建时间: {io3["time"].ToString()}");
                    await g.DownloadFileBigByUrl1(io3["dllname"].ToString(), io3["new"].ToString(), io3["pwd"].ToString(), "plugins/");
                    logger.Warn("已下载到plugins目录!请手动替换");
                }
                else
                {
                
                }

            }
        }
        catch (Exception r)
        {
            logger.Error(r.Message);
        }
      
    }


    }

