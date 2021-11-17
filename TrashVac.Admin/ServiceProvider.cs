using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashVac.Admin
{
    public class ServiceProvider
    {
        private static Services _services;

        public static Services Current => _services ??= new Services();
    }
}
