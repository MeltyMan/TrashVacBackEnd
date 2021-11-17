using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrashVac.Admin.Remote;

namespace TrashVac.Admin
{
    public class Services
    {
        private ApiClient _apiClient;

        public ApiClient ApiClient => _apiClient ??= new ApiClient();
    }
}
