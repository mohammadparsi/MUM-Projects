using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;


namespace Infrastructure
{
    public class Utility
    {

        public static string HMAC_MD5(string Key, string Value)
        {
            // The first two lines take the input values and convert them from strings to Byte arrays

            byte[] HMACkey = (new ASCIIEncoding()).GetBytes(Key);
            byte[] HMACdata = (new ASCIIEncoding()).GetBytes(Value);

            // create a HMACMD5 object with the key set
            HMACMD5 myhmacMD5 = new HMACMD5(HMACkey);

            // calculate the hash (returns a byte array)
            byte[] HMAChash = myhmacMD5.ComputeHash(HMACdata);

            // loop through the byte array and add append each piece to a string to obtain a hash string
            dynamic fingerprint = "";

            for (int i = 0; i <= HMAChash.Length - 1; i++)
            {
                fingerprint += HMAChash[i].ToString("x").PadLeft(2, '0');
            }
             
            return fingerprint;

        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}