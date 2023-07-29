
using LiteLoader.DynamicCommand;
using MC;
using Newtonsoft.Json.Linq;
using Skyblock.Common.Form;
using Skyblock.Logger;
using Skyblock.Stru;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Skyblock.Common
{
    internal class RegCommon
    {


        
        public LoadStru stru = new LoadStru();
   
        public  void  RegCommo() {

            DynamicCommandInstance instance = DynamicCommand.CreateCommand("is", "Dskyblock-主菜单", CommandPermissionLevel.Any);
            //    instance.Optional("m", DynamicCommand.ParameterType.Command,"我的空岛","my", CommandParameterOption.None);
         
            instance.AddOverload(new List<string>());
            instance.SetCallback((cmd, origin, output, results) =>
            {

                Form.Form j = new();
          j.msui(origin.Player);
            });
   DynamicCommand.Setup(instance);
    DynamicCommandInstance instanc = DynamicCommand.CreateCommand("ism", "我的信息", CommandPermissionLevel.Any);
       instanc.AddOverload(new List<string>());
         instanc.SetCallback((cmd, origin, output, results) =>
               {
                   if (!File.Exists($"plugins/SkyBlock/data/{origin.Player.Xuid}_{origin.Player.RealName}.json"))
                   {
                       Form.Form j = new();
                       j.CreativeMenu(origin.Player);
                   }
                   else
                   {
                       Form.Form j = new();
                       j.mymenu(origin.Player);
                   }

               });
           DynamicCommand.Setup(instanc);
            DynamicCommandInstance instanf = DynamicCommand.CreateCommand("isop", "管理员模式", CommandPermissionLevel.GameMasters);
           instanf.AddOverload(new List<string>());
           instanf.SetCallback((cmd, origin, output, results) =>
           {
               Form.Form j = new();
               j.opMager(origin.Player);
           });
           DynamicCommand.Setup(instanf);
            DynamicCommandInstance instanfe = DynamicCommand.CreateCommand("iscreative", "创建空岛", CommandPermissionLevel.GameMasters);
            instanfe.AddOverload(new List<string>());
            instanfe.SetCallback((cmd, origin, output, results) =>
            {
                Form.Form j = new();
                if (!File.Exists($"plugins/SkyBlock/data/{origin.Player.Xuid}_{origin.Player.RealName}.json"))
                {
                  j.  CreativeMenu(origin.Player);
                }
                else
                {
                    j.mymenu(origin.Player);
                }
            });
            DynamicCommand.Setup(instanfe);
        }
    }
}
