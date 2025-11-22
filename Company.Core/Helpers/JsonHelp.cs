using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Company.Core.Helper
{
    public static class JsonHelp
    {

        
        

     public  static T Read<T>(string jsonPath)
        {
            if(!File.Exists(jsonPath))
            {
                return default(T);
            }

            var json = File.ReadAllText(jsonPath);

            var result= Deserializer<T>(json);
            return result;

        }


        private static T Deserializer<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }


        public static void Write<T>(string jsonPath,T obj,bool extend)
        {
            string jsonString= Serializer<T>(obj,true);

            File.WriteAllText(jsonPath, jsonString);
        }

        private static string Serializer<T>(T obj,bool extend)
        {
            Formatting f= extend ? Formatting.Indented : Formatting.None;

            return JsonConvert.SerializeObject(obj,f);
        }

    }
}
