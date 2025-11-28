using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Core.Config
{
    public class ConfigManager : IConfigManager
    {
        private string root="Config";

        public static bool NoHardwareMode { get; set; } = true;
        /// <summary>
        /// 根据key获取配置文件路径 路径格式为 程序的根目录路径/Config/{key}.json
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetPath(ValueType key)
        {
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, root, key.ToString() + ".json");
        }
        /// <summary>
        /// 读取配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Read<T>(ValueType key)
        {
            var path = GetPath(key);
            var result= Helper.JsonHelp.Read<T>(path);
            return result;
        }
        /// <summary>
        /// 写入配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Write<T>(ValueType key, T value)
        {
            Directory.CreateDirectory(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, root));
            var path = GetPath(key);
            Helper.JsonHelp.Write<T>(path, value,true);
        }
    }
}
