//
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//
namespace DMTec.TestUIFramework.CommonAPI
{
    public class JsonHelper
    {
        public static bool WriteJsonFile<T>(string filePath, T t)
        {
            bool isOK = false;
            try
            {
                FileInfo fi = new FileInfo(filePath);
                var di = fi.Directory;
                if (!di.Exists) di.Create();

                using (StreamWriter stWriter = new StreamWriter(filePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Converters.Add(new JavaScriptDateTimeConverter());
                    serializer.NullValueHandling = NullValueHandling.Ignore;

                    JsonWriter jsWriter = new JsonTextWriter(stWriter);
                    serializer.Serialize(jsWriter, t);
                    jsWriter.Close();
                    stWriter.Close();
                }
                isOK = true;
            }
            catch (Exception ex)
            {
                isOK = false;
                Console.WriteLine(ex.Message.ToString());
                MessageBox.Show(ex.Message.ToString(), "Error Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return isOK;
        }

        public static object ReadJsonFile<T>(string filePath)
        {
            object obj = new object();
            try
            {
                FileInfo fi = new FileInfo(filePath);
                if (!fi.Exists) return null;

                using (StreamReader stReader = new StreamReader(filePath))
                {
                    JsonSerializer js = new JsonSerializer();
                    js.Converters.Add(new JavaScriptDateTimeConverter());
                    js.NullValueHandling = NullValueHandling.Ignore;
                    JsonReader jsReader = new JsonTextReader(stReader);
                    JObject jobj = (JObject)js.Deserialize(jsReader);
                    obj = JsonConvert.DeserializeObject<T>(jobj.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                MessageBox.Show(ex.Message.ToString());
                obj = null;
            }
            return obj;
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">DataClass Type</typeparam>
        /// <param name="t">DataClass Instance</param>
        /// <returns>Serialized String</returns>
        public static string GetJsonString<T>(T t)
        {
            string str = JsonConvert.SerializeObject(t);
            return str;
        }
    }
}
