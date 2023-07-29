using LiteLoader.Form;
using MC;
using Newtonsoft.Json.Linq;
using Skyblock.Logger;
using Skyblock.Stru;
using Form.val;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skyblock.Update.Get;
using Skyblock.RememberData;
namespace Skyblock.Common.Form
{
    internal class Form
    {
        private string yz = "plugins/SkyBlock/config.json";
        public SimpleForm ms = new SimpleForm("空岛菜单", "空岛菜单");
        public async void msui(Player player)
        {
            ms.Append(new Button("我的空岛", "textures/ui/purtle", (a) =>
            {
                if (!File.Exists($"plugins/SkyBlock/data/{a.Xuid}_{a.RealName}.json"))
                {
                    CreativeMenu(a);
                }
                else
                {
                    mymenu(player);
                }
            }));
            ms.Append(new Button("多人空岛", "textures/ui/anvil_icon", (a) =>
            {
                a.SendToastPacket("还没做", "还没做");
            }));
            ms.Append(new Button("空岛传送", "textures/ui/backup_replace", (a) =>
            {
                a.Runcmd("gmrule island tp");
            }));
            try
            {
                if (Convert.ToBoolean(FileV.ReadFile.Get("islandShop", yz).Result.ToString()) == true)
                {
                    ms.Append(new Button("空岛商店", "textures/ui/promo_gift_small_blue", (a) =>
                    {
                        var f = FileV.ReadFile.Get("shopcomm", "plugins/SkyBlock/config.json").Result.ToString();
                        a.Runcmd(f);
                    }));
                }
            }
            catch (Exception e)
            {
                logger.Warn(e);
            }
            ms.Append(new Button("空岛设置", "textures/ui/settings_glyph_color_2x", (a) =>
            {
                a.Runcmd("gmrule island");
            }));
            ms.Append(new Button("空岛共享", "textures/items/apple", (a) =>
            {
                shareland(player);
            }));
            ms.Callback = (a, ValueTask) =>
            { };
            ms.SendTo(player);
        }
        public void CreativeMenu(Player player)
        {
            player.SendModalForm("空岛菜单", "你还没有岛屿,请选择是否要创建空岛", "是", "否", (a) =>
            {
                if (a == true)
                {
                    ChooseLand(player);
                }
                else
                {
                    player.SendTextPacket("您取消了", TextType.Tip);
                }
            });
        }
        public SimpleForm cs = new("空岛选择", "");
        private LoadStru stru = new();
        public void ChooseLand(Player player)
        {
            cs.Append(new Button("经典岛屿\n§3§o经典的空岛"));
            cs.Append(new Button("双子岛屿\n§3§o两座岛,更多机遇"));
            cs.Append(new Button("极难岛屿\n§o§4选择之后,后果自负"));
            cs.Callback = (a, ValueTask) =>
            {
                switch (ValueTask)
                {
                    case 0:
                        stru.LoadStr(player, "1");
                        break;
                    case 1:
                        stru.LoadStr(player, "2");
                        break;
                    case 2:
                        stru.LoadStr(player, "3");
                        break;
                    default:
                        player.SendText("您什么也没有选择");
                        break;
                }
            };
            cs.SendTo(player);
        }
        public void mymenu(Player player)
        {
            var h = FileV.ReadFile.Get(player.RealName, $"plugins/SkyBlock/data/{player.Xuid}_{player.RealName}.json").Result;
            JObject ck = JObject.Parse(h.ToString());
            string zl = ck["zl"].ToString();
            string x = ck["x"].ToString();
            string y = ck["y"].ToString();
            string z = ck["z"].ToString();
            SimpleForm simple = new("个人空岛菜单", $"您好{player.RealName}\n这是您的一些空岛信息:\n种类: {zl}\nX坐标: {x} \nY坐标: {y}\nZ坐标: {z}");
            simple.AddButton("传送到空岛", "", (a) =>
            {
                try
                {
                    player.Teleport(new Vec3(int.Parse(x), int.Parse(y), int.Parse(z)), 0);
                }
                catch (Exception e)
                {
                    player.SendText("传送失败");
                    logger.Error(e.Message);
                }
            });
            simple.AddButton("完成", "", (a) =>
            {
                return;
            });
            simple.Callback = (a, b) =>
            { };
            simple.SendTo(player);
        }
        public void shareland(Player player)
        {
            SimpleForm ssc = new("空岛共享", "");
            ssc.AddButton("查看公共空岛", "", (a) =>
            {
                getShareland(a);
            });
            ssc.AddButton("公开我的空岛", "", (a) =>
            {
                logger.Info(Data.c[a.Xuid]);
                if (Data.c[a.Xuid] != null)
                {
                    Mainjjj(player);
                }
                else
                {
                    if (!File.Exists($"plugins/SkyBlock/data/{player.Xuid}_{player.RealName}.json"))
                    {
                        CreativeMenu(player);
                    }
                    else
                    {
                        shareland2(a);
                    }
                }
            });
            ssc.Callback = (a, c) =>
            { };
            ssc.SendTo(player);
        }
        public Dictionaryt dictionaryt = new Dictionaryt();
        public void shareland2(Player player)
        {
            CustomForm form = new("共享我的空岛");
            form.Append(new Input("input", "请输入共享空岛名称", player.RealName, $"{player.RealName}的空岛"));
            form.Callback = async (a, b) =>
            {
                var c = dictionaryt.GetData(b, "input");
                logger.Warn(c);
                if (c != null)
                {
                    var h = FileV.ReadFile.Get(player.RealName, $"plugins/SkyBlock/data/{player.Xuid}_{player.RealName}.json").Result;
                    JObject ck = JObject.Parse(h.ToString());
                    int x = (int)ck["x"];
                    int y = (int)ck["y"];
                    int z = (int)ck["z"];
                    try
                    {
                        Data.c.Remove(player.Xuid);
                        Data.c.Add(player.Xuid, new JObject(new JProperty("by", c), new JProperty("x", x), new JProperty("y", y), new JProperty("z", z)));
                    }
                    catch
                    {
                        player.SendText("分享失败");
                    }
                    await Data.ServerStopped1();
                    a.SendTextPacket("分享成功", TextType.Tip);
                }
                else
                {
                    a.SendTextPacket("你输入错误", TextType.Tip);
                }
            };
            form.SendTo(player);
        }
        public void getShareland(Player player)
        {
            SimpleForm simple = new SimpleForm("空岛共享", "");
            foreach (var c in Data.c)
            {
                try
                {
                    JObject sdc = JObject.Parse(c.Value.ToString());
                    simple.AddButton(sdc["by"].ToString(), "", (a) =>
                    {
                        a.Teleport(new Vec3(int.Parse(sdc["x"].ToString()), int.Parse(sdc["y"].ToString()), int.Parse(sdc["z"].ToString())), 0);
                    });
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            simple.Callback = (a, v) =>
            { };
            simple.SendTo(player);
        }
        public void Mainjjj(Player player)
        {
            SimpleForm simple = new SimpleForm("空岛共享", "您已经共享过了");
            simple.AddButton("更改共享信息", "", (a) =>
            {
                shareland2(a);
            });
            simple.AddButton("取消共享", "", async (a) =>
            {
                Data.c.Remove(a.Xuid);
                await Data.ServerStopped1();
            });
            simple.Callback = (v, k) =>
            { };
            simple.SendTo(player);
        }
        public void opMager(Player player)
        {
            SimpleForm simple = new SimpleForm("op菜单", "");
            SimpleForm simple1 = new SimpleForm("op菜单", "");
            simple.AddButton("管理玩家空岛", "", (a) =>
            {
                var j = Directory.GetFiles("plugins/SkyBlock/data/");
                foreach (var f in j)
                {
                    simple1.AddButton(f, "", (a) =>
                    {
                        opmager2(a, f);
                    });
                }
                simple1.Callback = (a, b) =>
                { };
                simple1.SendTo(player);
            });
            simple.Callback = (a, b) =>
            { };
            simple.SendTo(player);
        }
        public void opmager2(Player player, string x)
        {
            SimpleForm simpleForm = new SimpleForm("空岛菜单", "");
            simpleForm.AddButton("删除空岛", "", (a) =>
            {
                File.Delete(x);
                a.SendToastPacket("删除成功", "删除成功");
            });
            simpleForm.AddButton("传送到空岛", "", (a) =>
            {
                var f = File.ReadAllText(x.ToString());
                JObject gh1 = JObject.Parse(f);
                foreach (var i in gh1)
                {
                    JObject gh = JObject.Parse(i.Value.ToString());
                    player.Teleport(new Vec3(int.Parse(gh["x"].ToString()), int.Parse(gh["y"].ToString()), int.Parse(gh["z"].ToString())), 0);
                }
            });
            simpleForm.Callback = (a, b) =>
            { };
            simpleForm.SendTo(player);
        }
        private static Level level = new Level();
        Player ic;
        public void MoreLand(Player player)
        {
            SimpleForm simple = new SimpleForm("选择共享玩家", "");
            foreach (var i in Level.GetAllPlayers())
            {
                simple.AddButton(i.RealName, "", (a) =>
                {
                    try
                    {
                        ic = level.GetPlayer(i.RealName);
                    }
                    catch (Exception e)
                    {
                        player.SendText("对方不在线");
                        logger.Info(e);
                    }
                    if (!File.Exists($"plugins/SkyBlock/MoreLand/{ic.Xuid}_{ic.RealName}.json"))
                    {
                        ic.SendModalForm(player.RealName + "邀请你加入他的空岛", player.RealName + "邀请你加入他的空岛\n您是否同意", "同意", "不同意", (a2) =>
                        {
                            if (a2 == true)
                            {
                                try
                                {
                                    FileStream file = File.Create($"plugins/SkyBlock/MoreLand/{ic.Xuid}_{ic.RealName}.json");
                                    file.Close();
                                }
                                catch (Exception e)
                                {
                                    ic.SendText("加入失败");
                                    player.SendText("对方加入失败");
                                }
                            }
                        });
                    }
                    else
                    {
                        player.SendTextPacket("对方已经加入你的/别人的空岛了", TextType.Tip);
                    }
                });
            }
            simple.Callback = (a, v) =>
            { };
            simple.SendTo(player);
        }
    }
}