using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
namespace File_Crypto
{
    class Hash_Generation
    {
        public static byte[] Hash(string Password)
        {
            byte[] pkey;
            int lk;
            byte[] hash = new byte[1024];
            int incrementer = 0;
            int phash = 0; // phash significa position hash
            lk = Password.Length;
            pkey = Encoding.ASCII.GetBytes(Password);
            
            for (byte repetir = 0; repetir < 255; repetir++)
            {
                for (int kp = 0; kp < lk; kp++) // kp  significa key position
                {                               // lpk significa lenght key position
                    hash[phash] = (byte)(hash[phash] ^ (int)Abs(Round(Cos(incrementer)*1000)) ^ pkey[(int)Abs(Round(Sin((repetir*kp*lk*phash)))*1000)%lk]);
                    phash++;
                    if (phash >= 1024) phash = 0;
                    incrementer++;
                }
            }
            
            return hash;
        }
        
    }
}
