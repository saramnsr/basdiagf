using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace BasCommon_BO
{
    public static class Cryptage
    {

        /// <summary>
        /// Encrypt the given string using the specified key.
        /// </summary>
        /// <param name="strToEncrypt">The string to be encrypted.</param>
        /// <param name="strKey">The encryption key.</param>
        /// <returns>The encrypted string.</returns>
        public static byte[] Encrypt(string strToEncrypt, string strKey)
        {
            
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(Encoding.Unicode.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Encoding.Unicode.GetBytes(strToEncrypt);
                return objDESCrypto.CreateEncryptor().
                    TransformFinalBlock(byteBuff, 0, byteBuff.Length);
           
        }

        /// <summary>
        /// Decrypt the given string using the specified key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <param name="strKey">The decryption key.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(byte[] byteBuff, string strKey)
        {
            
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(Encoding.Unicode.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                string strDecrypted = Encoding.Unicode.GetString
                (objDESCrypto.CreateDecryptor().TransformFinalBlock
                (byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;
                return strDecrypted;
            
        }

        /*
        public static byte[] Encrypt(string txt, string sKey)
        {
            TripleDES DES3 = new TripleDESCryptoServiceProvider();
            byte[] bs = new byte[24];
            for (int i = 0; i < DES3.Key.Length && i < sKey.Length; i++)
            {
                bs[i] = Convert.ToByte(sKey[i]);
            }
            DES3.Key = bs;
            DES3.IV = new byte[] { 1, 6, 10, 4, 2, 6, 7, 8 };
            MemoryStream ms_out = new MemoryStream();
            byte[] b_in = Encoding.Unicode.GetBytes(txt);
            ms_out.SetLength(0);
            CryptoStream encStream = new CryptoStream(ms_out, DES3.CreateEncryptor(), CryptoStreamMode.Write);
            encStream.Write(b_in, 0, b_in.Length);
            byte[] b_out = ms_out.ToArray();
            encStream.Close();
            ms_out.Close();
            return b_out;
        }
        
        public static string Decrypt(byte[] b_in, string sKey)
        {
            TripleDES DES3 = new TripleDESCryptoServiceProvider();
            byte[] bs = new byte[24];
            for (int i = 0; i < DES3.Key.Length && i < sKey.Length; i++)
            {
                bs[i] = Convert.ToByte(sKey[i]);
            }
            DES3.Key = bs;
            DES3.IV = new byte[] { 1, 6, 10, 4, 2, 6, 7, 8 };
            MemoryStream ms_out = new MemoryStream();
            ms_out.SetLength(0);
            CryptoStream encStream = new CryptoStream(ms_out, DES3.CreateDecryptor(), CryptoStreamMode.Write);
            encStream.Write(b_in, 0, b_in.Length);
            byte[] b_out = ms_out.ToArray();
            string result = Encoding.Unicode.GetString(b_out);
            ms_out.Close();
            return result;
        }
    */

    }
}
