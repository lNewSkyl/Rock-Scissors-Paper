using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RockScissorsPaper
{
    public static class Randomizer
    {
        internal static readonly char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        public static byte[] GetBytes(int length)
        {
            byte[] random = new byte[sizeof(uint)];
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(random);
                    sb.Append(chars[BitConverter.ToUInt32(random, 0) % chars.Length]);
                }
            }
            return Encoding.ASCII.GetBytes(sb.ToString());
        }

        public static uint GetRandomNumber(int min, int max)
        {
            var umin = (uint)min;
            var umax = (uint)max;
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                uint i;
                byte[] buffer = new byte[sizeof(uint)];
                rng.GetBytes(buffer);
                i = BitConverter.ToUInt32(buffer, 0);
                return umin + (i % (umax + 1 - umin));
            }
        }

        public static string GetHash(byte[] key, string name)
        {
            using (var hmac = new HMACSHA256(key))
            {
                var nameByte = Encoding.ASCII.GetBytes(name);
                return ToAsc2String(hmac.ComputeHash(nameByte));
            }
        }

        public static string ToAsc2String(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
