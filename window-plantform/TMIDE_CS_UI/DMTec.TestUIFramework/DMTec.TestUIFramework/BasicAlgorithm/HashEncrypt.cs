﻿using System;
using System.IO;
using System.Data;
using System.Text;
using System.Diagnostics;
using System.Security;
using System.Security.Cryptography;
/**/
/* 
* .Net框架由于拥有CLR提供的丰富库支持，只需很少的代码即可实现先前使用C等旧式语言很难实现的加密算法。
 * 本类实现一些常用机密算法，供参考。
 * 其中MD5算法返回Int的ToString字串。
*/
namespace DMTec.TestUIFramework.BasicAlgorithm
{
    /**/
    /// <summary> 
    /// 类名：HashEncrypt 
    /// 作用：对传入的字符串进行Hash运算，返回通过Hash算法加密过的字串。 
    /// 属性：［无］ 
    /// 构造函数额参数： 
    /// IsReturnNum:是否返回为加密后字符的Byte代码 
    /// IsCaseSensitive：是否区分大小写。 
    /// 方法：此类提供MD5，SHA1，SHA256，SHA512等四种算法，加密字串的长度依次增大。 
    /// </summary> 
    public class HashEncrypt
    {
        //private string strIN; 
        private bool isReturnNum;
        private bool isCaseSensitive;
        /**/
        /// <summary> 
        /// 类初始化，此类提供MD5，SHA1，SHA256，SHA512等四种算法，加密字串的长度依次增大。 
        /// </summary> 
        /// <param name="IsCaseSensitive">是否区分大小写</param> 
        /// <param name="IsReturnNum">是否返回为加密后字符的Byte代码</param> 
        public HashEncrypt(bool IsCaseSensitive, bool IsReturnNum)
        {
            this.isReturnNum = IsReturnNum;
            this.isCaseSensitive = IsCaseSensitive;
        }
        private string getstrIN(string strIN)
        {
            //string strIN = strIN; 
            if (strIN.Length == 0)
            {
                strIN = "~NULL~";
            }
            if (isCaseSensitive == false)
            {
                strIN = strIN.ToUpper();
            }
            return strIN;
        }
        public string MD5Encrypt(string strIN)
        {
            //string strIN = getstrIN(strIN); 
            byte[] tmpByte;
            MD5 md5 = new MD5CryptoServiceProvider();
            tmpByte = md5.ComputeHash(GetKeyByteArray(getstrIN(strIN)));
            md5.Clear();
            return GetStringValue(tmpByte);
        }
        public string SHA1Encrypt(string strIN)
        {
            //string strIN = getstrIN(strIN); 
            byte[] tmpByte;
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            tmpByte = sha1.ComputeHash(GetKeyByteArray(strIN));
            sha1.Clear();
            return GetStringValue(tmpByte);
        }
        public string SHA256Encrypt(string strIN)
        {
            //string strIN = getstrIN(strIN); 
            byte[] tmpByte;
            SHA256 sha256 = new SHA256Managed();
            tmpByte = sha256.ComputeHash(GetKeyByteArray(strIN));
            sha256.Clear();
            return GetStringValue(tmpByte);
        }
        public string SHA512Encrypt(string strIN)
        {
            //string strIN = getstrIN(strIN); 
            byte[] tmpByte;
            SHA512 sha512 = new SHA512Managed();
            tmpByte = sha512.ComputeHash(GetKeyByteArray(strIN));
            sha512.Clear();
            return GetStringValue(tmpByte);
        }
        /**/
        /// <summary> 
        /// 使用DES加密（Added by niehl 2005-4-6） 
        /// </summary> 
        /// <param name="originalValue">待加密的字符串</param> 
        /// <param name="key">密钥(最大长度8)</param> 
        /// <param name="IV">初始化向量(最大长度8)</param> 
        /// <returns>加密后的字符串</returns> 
        public string DESEncrypt(string originalValue, string key, string IV)
        {
            //将key和IV处理成8个字符 
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);
            SymmetricAlgorithm sa;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ct = sa.CreateEncryptor();
            byt = Encoding.UTF8.GetBytes(originalValue);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }
        public string DESEncrypt(string originalValue, string key)
        {
            return DESEncrypt(originalValue, key, key);
        }
        /**/
        /// <summary> 
        /// 使用DES解密（Added by niehl 2005-4-6） 
        /// </summary> 
        /// <param name="encryptedValue">待解密的字符串</param> 
        /// <param name="key">密钥(最大长度8)</param> 
        /// <param name="IV">m初始化向量(最大长度8)</param> 
        /// <returns>解密后的字符串</returns> 
        public string DESDecrypt(string encryptedValue, string key, string IV)
        {
            //将key和IV处理成8个字符 
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);
            SymmetricAlgorithm sa;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ct = sa.CreateDecryptor();
            byt = Convert.FromBase64String(encryptedValue);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Encoding.UTF8.GetString(ms.ToArray());
        }
        public string DESDecrypt(string encryptedValue, string key)
        {
            return DESDecrypt(encryptedValue, key, key);
        }
        private string GetStringValue(byte[] Byte)
        {
            string tmpString = "";
            if (this.isReturnNum == false)
            {
                ASCIIEncoding Asc = new ASCIIEncoding();
                tmpString = Asc.GetString(Byte);
            }
            else
            {
                int iCounter;
                for (iCounter = 0; iCounter < Byte.Length; iCounter++)
                {
                    tmpString = tmpString + Byte[iCounter].ToString();
                }
            }
            return tmpString;
        }
        private byte[] GetKeyByteArray(string strKey)
        {
            ASCIIEncoding Asc = new ASCIIEncoding();
            int tmpStrLen = strKey.Length;
            byte[] tmpByte = new byte[tmpStrLen - 1];
            tmpByte = Asc.GetBytes(strKey);
            return tmpByte;
        }
    }
}