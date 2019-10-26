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
    public interface IKeys {
         IEncryptionProvider _provider { get; set; }
    }

    public class Keys : KeysAbstract, IKeys
    {
        public IEncryptionProvider _provider { get; set; }

       
        public Keys(IGetKeys getkeys)
        {

             _provider =  new AesProvider(Convert.FromBase64String(getkeys.Key1), Convert.FromBase64String(getkeys.Key2));
        }

    }

    public interface IGetKeys
    {
        string Key1 { get; set; }
        string Key2 { get; set; }

    }

    public class GetKeys : IGetKeys
    {
        public GetKeys(IConfiguration configuration, string key1, string key2)
        {
            Key1 = key1;
            Key2 = key2;
        }
        public string Key1 { get; set; }
        public string Key2 { get; set; }

    }


    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class EncryptedAttribute : Attribute
    { }

    class EncryptedConverter : ValueConverter<string, string>
    {
        public EncryptedConverter(Expression<Func<string, string>> EncryptExpr, Expression<Func<string, string>> DecryptExpr, ConverterMappingHints mappingHints = default)
            : base(EncryptExpr, DecryptExpr, mappingHints)
        {
     
        }
        
    }

     
}