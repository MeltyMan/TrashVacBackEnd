using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace KYH_Kalle.Core.Configurations
{
    public class ConnectionStringsConfiguration : AConfigBase
    {
        public ConnectionStringsConfiguration(IConfigurationRoot configurationRoot) : base(configurationRoot)
        {
            _root = "ConnectionStrings";

        }


        private readonly string _root;
        public string KYHConnectionString => GetConfigValue(_root, "KYHConnectionString");
    }
}
