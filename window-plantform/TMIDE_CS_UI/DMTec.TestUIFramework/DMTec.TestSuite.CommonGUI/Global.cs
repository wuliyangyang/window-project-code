using DMTec.TestUIFramework.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTec.TestSuite.CommonGUI
{
    class Global
    {
        private static Global g = new Global();
        private static string _path;
        private Global()
        {
            GetConfigInstance();
        }
        public static Global GetGlobalInstance()
        {
            return g;
        }
        public string GetPath()
        {
            return _path;
        }
        public static SystemConfigInfo GetConfigInstance()
        {
            string workPath = System.Environment.CurrentDirectory;
            string path1 = workPath + "\\TesterUI\\SysConfig.json";    //debug路径
            string path2 = workPath + "\\ProxUI\\TesterUI\\SysConfig.json";   //release路径
            SystemConfigInfo myConfig = GetSysConfig(path1);

            if (null== myConfig)
            {
                myConfig = GetSysConfig(path2);
                if (null == myConfig) return null;
                CheckPath(myConfig, workPath);
                myConfig.ZmqPortConfigFilePath = workPath + "\\ProxUI\\" + myConfig.ZmqPortConfigFilePath;
                _path = workPath + "\\ProxUI\\";
                return myConfig;
            }
            else
            {
                _path = workPath;
                CheckPath(myConfig, workPath);
                myConfig.ZmqPortConfigFilePath = workPath + myConfig.ZmqPortConfigFilePath;
            }
                 
            return myConfig;
        }
        private static void CheckPath(SystemConfigInfo sc,string workPath)
        {
#if DEBUG
            sc.CsvFilePath = workPath + "\\TesterUI\\" + sc.CsvFilePath;
#else
            sc.CsvFilePath = workPath + "\\Profile\\" + sc.CsvFilePath;
#endif
        }
        private static SystemConfigInfo GetSysConfig(string path)
        {
            try
            {
                SystemConfigInfo cfg = new SystemConfigInfo();
                cfg = DMTec.TestUIFramework.CommonAPI.JsonHelper.ReadJsonFile<SystemConfigInfo>(path) as SystemConfigInfo;
                return cfg;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
