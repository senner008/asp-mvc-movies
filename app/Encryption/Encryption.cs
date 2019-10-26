using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using asp_mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace MvcMovie.Models
{
    public interface IKeys {}

    public class Keys : IKeys
    {
        public static IEncryptionProvider _provider { get; set; }

        private static readonly HashSet<char> _base64Characters = new HashSet<char>() { 
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 
            'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 
            'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 
            'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/', 
            '='
        };

        public Keys(IGetKeys getkeys)
        {

             _provider =  new AesProvider(Convert.FromBase64String(getkeys.Key1), Convert.FromBase64String(getkeys.Key2));
        }

        public static bool IsBase64String(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            else if (value.Any(c => !_base64Characters.Contains(c)))
            {
                return false;
            }

            try
            {
                Convert.FromBase64String(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }

    public interface IGetKeys
    {
        string Key1 { get; set; }
        string Key2 { get; set; }

    }

    public class GetKeys : IGetKeys
    {
        public GetKeys(IConfiguration configuration)
        {
            Boolean isProduction = Environment.GetEnvironmentVariable ("ASPNETCORE_ENVIRONMENT") == "Production";
            if (isProduction) {
                Key1 = Environment.GetEnvironmentVariable("AES_KEY1");
                Key2 = Environment.GetEnvironmentVariable("AES_KEY2");
            } else {
                 var keys = configuration.GetSection("Passwords");
                Key1 = keys.GetSection("encryptionKey").Value;
                Key2 = keys.GetSection("encryptionIV").Value;
            }
        }
        public string Key1 { get; set; }
        public string Key2 { get; set; }

    }


    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class EncryptedAttribute : Attribute
    { }

    class EncryptedConverter : ValueConverter<string, string>
    {
        public EncryptedConverter(ConverterMappingHints mappingHints = default)
            : base(EncryptExpr, DecryptExpr, mappingHints)
        { 
      
        }

        static Expression<Func<string, string>> DecryptExpr = x => Keys.IsBase64String(x) ? Keys._provider.Decrypt(x) : x;
        static Expression<Func<string, string>> EncryptExpr = x => Keys._provider.Encrypt(x);
    }

     
}