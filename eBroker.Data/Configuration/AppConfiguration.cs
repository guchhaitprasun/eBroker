using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eBroker.Data.Configuration
{
    public class AppConfiguration
    {
        public AppConfiguration()
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configBuilder.AddJsonFile(path, false);
            var root = configBuilder.Build();
            var appSetting = root.GetSection("ConnectionStrings:DefaultConnection");

            SqlConnectionString = appSetting.Value;
        }
        public string SqlConnectionString { get; set; }
    }
}
