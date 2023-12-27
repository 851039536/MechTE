using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MechTE_480.util
{
    /// <summary>
    /// DES加密/解密类。
    /// </summary>
    public class MEncrypt
    {

        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        [Obsolete]
        public static string Encrypt(string Text)
        {
            return Encrypt(Text,"MATICSOFT");
        }

        /// <summary> 
        /// md5加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        [Obsolete]
        public static string Encrypt(string Text,string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = Encoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey,"md5").Substring(0,8));
            des.IV = Encoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey,"md5").Substring(0,8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms,des.CreateEncryptor(),CryptoStreamMode.Write);
            cs.Write(inputByteArray,0,inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}",b);
            }
            return ret.ToString();
        }


        /// <summary>
        /// md5解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        [Obsolete]
        public static string Decrypt(string Text)
        {
            return Decrypt(Text,"MATICSOFT");
        }

        /// <summary> 
        /// md5解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        [Obsolete]
        public static string Decrypt(string Text,string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0 ; x < len ; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2,2),16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = Encoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey,"md5").Substring(0,8));
            des.IV = Encoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey,"md5").Substring(0,8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms,des.CreateDecryptor(),CryptoStreamMode.Write);
            cs.Write(inputByteArray,0,inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }


        //密钥
        private static readonly byte[] ArrDesKey = new byte[] { 42,16,93,156,78,4,218,32 };
        private static readonly byte[] ArrDesiv = new byte[] { 55,103,246,79,36,99,167,3 };

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="mNeedEncodeString"></param>
        /// <returns></returns>
        public static string Encode(string mNeedEncodeString)
        {
            if (mNeedEncodeString == null)
            {
                throw new Exception("Error: \n源字符串为空！！");
            }
            DESCryptoServiceProvider objDes = new DESCryptoServiceProvider();
            MemoryStream objMemoryStream = new MemoryStream();
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream,objDes.CreateEncryptor(ArrDesKey,ArrDesiv),CryptoStreamMode.Write);
            StreamWriter objStreamWriter = new StreamWriter(objCryptoStream);
            objStreamWriter.Write(mNeedEncodeString);
            objStreamWriter.Flush();
            objCryptoStream.FlushFinalBlock();
            objMemoryStream.Flush();
            return Convert.ToBase64String(objMemoryStream.GetBuffer(),0,(int)objMemoryStream.Length);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="mNeedEncodeString"></param>
        /// <returns></returns>
        public static string Decode(string mNeedEncodeString)
        {
            if (mNeedEncodeString == null)
            {
                throw new Exception("Error: \n源字符串为空！！");
            }
            DESCryptoServiceProvider objDes = new DESCryptoServiceProvider();
            byte[] arrInput = Convert.FromBase64String(mNeedEncodeString);
            MemoryStream objMemoryStream = new MemoryStream(arrInput);
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream,objDes.CreateDecryptor(ArrDesKey,ArrDesiv),CryptoStreamMode.Read);
            StreamReader objStreamReader = new StreamReader(objCryptoStream);
            return objStreamReader.ReadToEnd();
        }
    }
}
