using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace KYH_Kalle.Core
{
    public class Services
    {
        private Configuration _configuration;
        public void InitServices(IConfigurationBuilder builder)
        {
            _configuration = new Configuration(builder);
        }
        public Configuration Configuration => _configuration;
    }
}
