using System;
using System.IO;
using System.Text;

namespace AmongUsRegionReplacerConsole
{
    /// <summary>
    /// ©2025 XtremeWave - Infinitive. All rights reserved.
    /// 使用前请查看README.md了解更多信息。
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Xtreme Server 安装工具");
            Console.WriteLine("=============================");
            Console.WriteLine("按任意键继续...");
            Console.ReadKey();

            try
            {
                ReplaceRegionInfo();
                Console.WriteLine("操作完成，按任意键退出...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"错误: {ex.Message}");
                Console.WriteLine("按任意键退出...");
            }

            Console.ReadKey();
        }

        static void ReplaceRegionInfo()
        {
            // 获取LocalLow文件夹路径[1,5](@ref)
            string localLowPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            localLowPath = Path.Combine(localLowPath, "..", "LocalLow");
            localLowPath = Path.GetFullPath(localLowPath);

            Console.WriteLine($"LocalLow路径: {localLowPath}");

            // 构建目标路径[1](@ref)
            string amongUsPath = Path.Combine(localLowPath, "Innersloth", "Among Us");
            string targetFile = Path.Combine(amongUsPath, "regionInfo.json");

            Console.WriteLine($"目标文件: {targetFile}");

            // 确保目录存在
            if (!Directory.Exists(amongUsPath))
            {
                Directory.CreateDirectory(amongUsPath);
                Console.WriteLine("创建Among Us配置目录");
            }

            // JSON内容
            string jsonContent = @"{
    ""CurrentRegionIdx"": 3,
    ""Regions"": [
        {
            ""$type"": ""StaticHttpRegionInfo, Assembly-CSharp"",
            ""Name"": ""<color=#cdfffd>XtremeWave</color>.<color=#FFFF00>HongKong</color>"",
            ""PingServer"": ""imp.hayashiume.top"",
            ""Servers"": [
                {
                    ""Name"": ""http-1"",
                    ""Ip"": ""https://imp.hayashiume.top"",
                    ""Port"": 443,
                    ""UseDtls"": false
                }
            ],
            ""TranslateName"": 1003
        }
    ]
}";

            // 备份原文件
            if (File.Exists(targetFile))
            {
                string backupFile = targetFile + ".backup_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                File.Copy(targetFile, backupFile);
                Console.WriteLine($"已备份原文件: {backupFile}");
            }

            // 写入新文件
            File.WriteAllText(targetFile, jsonContent, Encoding.UTF8);
            Console.WriteLine("regionInfo.json文件替换成功！");
        }
    }
}