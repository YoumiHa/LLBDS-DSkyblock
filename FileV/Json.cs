using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    internal class ConfigT
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">patch</param>
        /// <param name="b">data1</param>
        /// <param name="c">data2</param>
        /// <param name="c">data3</param>
        /// <returns></returns>
        internal static Task<bool> Set(string a,string b,string c,string d)
        {
            if (!File.Exists(a))
            {
                FileStream fs = File.Create(a);//创建文件
                fs.Close();
                File.WriteAllText(a, "{}");
                string json = File.ReadAllText(a);
                JObject obj = JObject.Parse(json);
                try
                {
                    obj.Remove(b);
                }
                catch
                {

                }
                obj.Add(new JProperty(b, new JObject(
                    new JProperty("text", c),
                    new JProperty("comm",d) 
                    )
                 )) ;
                File.WriteAllText(a, obj.ToString());

                return Task.FromResult(true);
            }
            else
            {
                string json = File.ReadAllText(a);

                JObject obj = JObject.Parse(json);
                try
                {
                    obj.Remove(b);
                }
                catch
                {

                }
                obj.Add(new JProperty(b, new JObject(
                    new JProperty("text", c),
                    new JProperty("comm", d))
                 ));
                File.WriteAllText(a, obj.ToString());

                return Task.FromResult(true);
            }

            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">文件路径</param>
        /// <param name="cd"></param>
        /// <returns></returns>
        internal static Task Get(string c)
        {
             if (!File.Exists(c))
            {
                FileStream fs = File.Create(c);//创建文件
                fs.Close();
                File.WriteAllText(c, "{}");
                return Task.FromResult(false);
            }
            else if (File.ReadAllText(c) != null)
            {
               return Task<JObject>.FromResult(JObject.Parse(File.ReadAllText(c)));
            }
            else
            {
                return Task.FromResult(false);
            }

        }
              
            
    }
}
