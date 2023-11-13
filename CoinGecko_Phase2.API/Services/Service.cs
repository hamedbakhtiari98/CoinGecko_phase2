using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;




namespace CoinGecko_Phase2.API

{
    public static class Service
    {
        public static string HashPass(string password)
        {

            byte[] salt = new byte[1];
            salt[0] = 0;

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
