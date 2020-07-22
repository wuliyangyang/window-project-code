using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DMTec.TestUIFramework.CommonAPI
{

    public class IniHelper
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);
        //注：
        //  section：要读取的段落名
        //key: 要读取的键
        //defVal: 读取异常的情况下的缺省值
        //retVal: key所对应的值，如果该key不存在则返回空值
        //size: 值允许的大小
        //filePath: INI文件的完整路径和文件名

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        //注：
        //  section: 要写入的段落名
        //key: 要写入的键，如果该key存在则覆盖写入
        //val: key所对应的值
        //filePath: INI文件的完整路径和文件名

        public static string IniReadValue(string section, string skey, string path)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(section, skey, "", temp, 500, path);
            return temp.ToString();
        }

        public static void IniWrite(string section, string key, string value, string path)
        {
            WritePrivateProfileString(section, key, value, path);
        }
    }
}
