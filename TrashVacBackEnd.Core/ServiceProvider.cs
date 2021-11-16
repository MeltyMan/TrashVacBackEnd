using System;
using System.Collections.Generic;
using System.Text;

namespace TrashVacBackEnd.Core
{
    public class ServiceProvider
    {
        private static Services _services;

        public static Services Current => _services ??= new Services();
    }
}
