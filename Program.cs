using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarRailUnlock120
{
    internal class Program
    {
        public static void Main()
        {
            var node = Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("miHoYo")?.OpenSubKey("崩坏：星穹铁道", true);
            if (node == null)
            {
                Console.WriteLine("打开注册表失败，请尝试至少运行一次游戏");
                Console.ReadLine();
                return;
            }
            var keyName = node.GetValueNames().FirstOrDefault(x => x.StartsWith("MIHOYOSDK_CONFIG_MODEL_NAME"));
            if (keyName == default)
            {
                Console.WriteLine("获取注册表内容失败，请尝试至少运行一次游戏");
                Console.ReadLine();
                return;
            }
            var key = node.GetValue(keyName);
            if (key == null)
            {
                Console.WriteLine("获取注册表内容失败，请尝试至少运行一次游戏");
                Console.ReadLine();
                return;
            }
            var value = Encoding.UTF8.GetString((byte[])key);
            var json = JObject.Parse(value);
            json["FPS"] = 120;
            node.SetValue(keyName, Encoding.UTF8.GetBytes(json.ToString(Newtonsoft.Json.Formatting.None)));
            Console.WriteLine("设置完成");
            Console.ReadLine();
        }
    }
}
