using System.Security.Cryptography;


namespace Hotel.App.API2
{
     public class RsaKeyHelper
    {
        public static RSAParameters GenerateKey()
        {
            using (var key = new RSACryptoServiceProvider(2048))
            {
                return key.ExportParameters(true);
            }
        }
    }
}