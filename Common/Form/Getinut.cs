using LiteLoader.Form;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form.val
{
    public class Dictionaryt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">CustomForm返回值val</param>
        /// <param name="a">读取的值string</param>
        /// <returns></returns>
        public object GetData(Dictionary<string, CustomFormElement> key, string a)
        {
            try
            {
                key.TryGetValue(a, out CustomFormElement value);
                if (value.Value == "")
                {
                    return null;
                }
                else
                {
                    return value.Value.ToString();
                }
            }catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
           
          
        }
    }
}
