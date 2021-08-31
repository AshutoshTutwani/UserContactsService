using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Contact.Domain.Models;
using Newtonsoft.Json;

namespace Contact.Domain.Helper
{
    public static class DomainHelper
    {
        #region Fields and Constants

        private const string JsonFileName = "userData.json";

        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return System.IO.Path.GetDirectoryName(path);
            }
        }

        #endregion

        #region Methods

        internal static List<UserDetail> DeserializeData()
        {
            if (!File.Exists(Path.Combine(AssemblyDirectory, JsonFileName)))
            {
                return null;
            }

            //Read file to string
            string json = File.ReadAllText(Path.Combine(AssemblyDirectory, JsonFileName));
            //dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            return JsonConvert.DeserializeObject<List<UserDetail>>(json);
        }

        internal static bool SerializeData(List<UserDetail> usersList)
        {
            var json = JsonConvert.SerializeObject(usersList, Formatting.Indented);
            File.WriteAllText(Path.Combine(AssemblyDirectory, JsonFileName), json);
            return true;
        }

        #endregion
    }
}