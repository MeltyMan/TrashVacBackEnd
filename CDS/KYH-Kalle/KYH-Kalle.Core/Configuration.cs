using System;
using System.Collections.Generic;
using System.Text;
using KYH_Kalle.Core.Configurations;
using Microsoft.Extensions.Configuration;

namespace KYH_Kalle.Core
{
    public class Configuration : AConfigBase
    {
        public Configuration(IConfigurationBuilder builder)
        {
            _configuration = builder.Build();
            ConnectionStrings = new ConnectionStringsConfiguration(_configuration);

        }

        public ConnectionStringsConfiguration ConnectionStrings { get; private set; }
    }
}
