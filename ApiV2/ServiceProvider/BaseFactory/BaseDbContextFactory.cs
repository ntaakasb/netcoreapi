using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ApiV2.ServiceProvider.BaseFactory.Interface;
using ServiceContext.Provider;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Cryptography;
using ServiceContext.Provider.SQL;

namespace ApiV2.ServiceProvider.BaseFactory
{
    public class BaseDbContextFactory : IBaseDbContextFactory
    {
        private readonly IConfiguration _config;
        private string key = "12313231trfssdadad@hSDaaaa321313";

        public BaseDbContextFactory(IConfiguration config)
        {
            _config = config;
        }
        public RepositoryDbContext GetInstance()
        {
            string connectionString = this.GetConnectionString();
            return new RepositoryDbContext(connectionString, _config);
        }

        private String GetConnectionString()
        {
            var ConfigConnectionString = _config.GetConnectionString("DefaultConnection");
            var ConfigIsEncodeConnection = _config["IsEncodeConnection"];
            var ConfigIsUseMyKey = _config["IsUseMyKey"];
            bool IsEncoded = false;
            bool IsUseMyKey = false;
            if (ConfigIsUseMyKey != null)
                IsUseMyKey = ConfigIsUseMyKey.ToString() == "1" ? true : false;
            if (ConfigIsEncodeConnection!= null)
                IsEncoded = ConfigIsEncodeConnection.ToString() == "1" ? true : false;
            if (ConfigConnectionString != null)
            {
                if (IsEncoded)
                    return Decode(ConfigConnectionString.ToString(), IsUseMyKey);
                else
                {
                    return ConfigConnectionString.ToString();
                }
            }
            else
                return "Data Source=127.0.0.1\\SQL2014;User ID=sa;Password=123456;Initial Catalog=catalogdefault;";
        }

        private string Decode(string text, bool useMyKey = false, bool useHashing = true, CipherMode ciphermode = CipherMode.ECB, PaddingMode paddingmode = PaddingMode.PKCS7)
        {
            if (useMyKey)
            {
                var ValueKey = _config["HasheKeyDBContext"];
                if (ValueKey != null)
                {
                    key = ValueKey.ToString();
                }
            }
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(text);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = ciphermode;
            tdes.Padding = paddingmode;
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return UTF32Encoding.UTF8.GetString(resultArray);
        }

        private string Encode(string text, bool useMyKey = false, bool useHashing = true, CipherMode ciphermode = CipherMode.ECB, PaddingMode paddingmode = PaddingMode.PKCS7)
        {
            if (useMyKey)
            {
                var ValueKey = _config["HasheKeyDBContext"];
                if (ValueKey != null)
                {
                    key = ValueKey.ToString();
                }
            }
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(text);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = ciphermode;
            tdes.Padding = paddingmode;
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
    }
}