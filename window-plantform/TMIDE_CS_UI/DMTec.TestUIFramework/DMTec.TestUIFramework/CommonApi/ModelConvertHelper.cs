using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Data;
using System.Reflection;
using System.Windows.Forms;
//
namespace DMTec.TestUIFramework.CommonAPI
{
    /// <summary>
    /// ModelConvertHelper Class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelConvertHelper<T> where T : new()
    {
        /// <summary>
        /// DataTable to List<T>.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> ConvertToModel(DataTable dt)
        {
            try
            {
                IList<T> tempList = new List<T>();
                Type type = typeof(T);
                string pName = "";
                foreach (DataRow dr in dt.Rows)
                {
                    T temp = new T();
                    PropertyInfo[] properties = temp.GetType().GetProperties();
                    foreach (PropertyInfo pi in properties)
                    {
                        pName = pi.Name;
                        if (dt.Columns.Contains(pName))
                        {
                            if (!pi.CanWrite) continue;
                            object value = dr[pName];
                            if (value != DBNull.Value) pi.SetValue(temp, value, null);
                        }
                    }
                    tempList.Add(temp);
                }
                return tempList;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
