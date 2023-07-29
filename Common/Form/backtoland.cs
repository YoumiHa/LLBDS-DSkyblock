using LiteLoader;
using LiteLoader.NET;
using LiteLoader.RemoteCall;
using MC;
using Skyblock.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Skyblock.Common.Form
{
    internal static class backtoland
    {
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerxuid">玩家xuid</param>
        /// <param name="pos1">posA</param>
        /// <param name="pos2">posB</param>
        /// <param name="a">维度</param>
        /// <returns></returns>
        public static string LANDapi(string playerxuid, Vec3 pos1, Vec3 pos2, int a)
        {
            List<Valuetype> ac = new List<Valuetype>()
            {
                new Valuetype(playerxuid),
                new Valuetype(pos1),
                new Valuetype(pos2),
                new Valuetype(a)
            };
        var  i =     RemoteCallAPI.ImportFunc("ISlandruleApi", "CreateLand").Invoke(ac);
            i.AsValue(out Value ad );
            ad.AsString(out string xa);
            logger.Info(xa);
            return xa;
        }
       }
    }
