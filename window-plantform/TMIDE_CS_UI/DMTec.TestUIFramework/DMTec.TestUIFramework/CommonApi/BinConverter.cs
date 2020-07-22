using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows.Forms;
//
namespace DMTec.TestUIFramework.CommonAPI
{
    /// <summary>
    /// This Class Help us to convert class object to binary file.
    /// Or read binary file to class object.
    /// </summary>
    public class BinConverter
    {

        public static bool WriteBinFile<T>(string filePath, T t)
        {
            //Check if the file is exist.
            FileInfo fi = new FileInfo(filePath);
            var di = fi.Directory;
            if (!di.Exists) di.Create();

            bool isOK = false;
            Stream st = new FileStream(filePath, System.IO.FileMode.Create);
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                
                binaryFormatter.Serialize(st, t);//Serialization object class and input stream.  

                isOK = true;
            }
            catch(Exception ex)
            {
                isOK = false;
            }
            finally
            {
                st.Close();
            }

            return isOK;   
        }

        public static object ReadBinFile<T>(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            if (!fi.Exists) return null;

            object obj = new object();
            System.IO.Stream st = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                obj = (T)binaryFormatter.Deserialize(st);//De serialization and get one object then convert to class.
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                obj = null;
            }
            finally
            {
                st.Close();  
            }
            return obj;
        }

    }
}
