using System;
using System.IO;
using System.Security.Cryptography;

namespace MechTE_480.EncryptionCategory
{
    /// <summary>
    /// DES加密/解密类。
    /// </summary>
    public static class MDecUtil
    {
        //密钥
        private static readonly byte[] SArrDesKey = { 42,16,93,156,78,4,218,32 };
        private static readonly byte[] SArrDesiv = { 55,103,246,79,36,99,167,3 };

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
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream,objDes.CreateEncryptor(SArrDesKey,SArrDesiv),CryptoStreamMode.Write);
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
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream,objDes.CreateDecryptor(SArrDesKey,SArrDesiv),CryptoStreamMode.Read);
            StreamReader objStreamReader = new StreamReader(objCryptoStream);
            return objStreamReader.ReadToEnd();
        }
    }
}
