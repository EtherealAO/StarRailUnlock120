using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
                Console.WriteLine("打开国服注册表失败，请尝试至少在游戏中修改一次图形设置并关闭设置界面");
                Console.ReadLine();
                return;
            }
            var keyName = node.GetValueNames().FirstOrDefault(x => x.StartsWith("GraphicsSettings_Model"));
            if (keyName == default)
            {
                Console.WriteLine("获取国服注册表内容失败，请尝试至少在游戏中修改一次图形设置并关闭设置界面");
                Console.ReadLine();
                return;
            }
            var key = node.GetValue(keyName);
            if (key == null)
            {
                Console.WriteLine("获取国服注册表内容失败，请尝试至少在游戏中修改一次图形设置并关闭设置界面");
                Console.ReadLine();
                return;
            }
            var value = Encoding.UTF8.GetString((byte[])key);
            var json = JObject.Parse(value);
            json["FPS"] = 120;
            node.SetValue(keyName, Encoding.UTF8.GetBytes(json.ToString(Newtonsoft.Json.Formatting.None)));
            Console.WriteLine("国服设置完成");

            var intlNode = Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("Cognosphere")?.OpenSubKey("Star Rail", true);
            if (intlNode != null)
            {
                var intlKeyName = intlNode.GetValueNames().FirstOrDefault(x => x.StartsWith("GraphicsSettings_Model"));
                if (intlKeyName == null)
                {
                    Console.WriteLine("获取国际服注册表内容失败，请尝试至少在游戏中修改一次图形设置并关闭设置界面");
                    Console.ReadLine();
                    return;
                }
                var intlKey = intlNode.GetValue(intlKeyName);
                if (intlKey == null)
                {
                    Console.WriteLine("获取国际服注册表内容失败，请尝试至少在游戏中修改一次图形设置并关闭设置界面");
                    Console.ReadLine();
                    return;
                }
                var intlValue = Encoding.UTF8.GetString((byte[])intlKey);
                var intlJson = JObject.Parse(intlValue);
                intlJson["FPS"] = 120;
                intlNode.SetValue(intlKeyName, Encoding.UTF8.GetBytes(intlJson.ToString(Newtonsoft.Json.Formatting.None)));
                Console.WriteLine("国际服设置完成");
            }

            Console.ReadLine();
        }
    }
}
