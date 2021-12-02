using System;
using System.Collections.Generic;
using System.Text;
using KYH_Kalle.Core.Repository;

namespace KYH_Kalle.Core
{
    public class Injector
    {
        public static T GetInstance<T>()
        {
            var typeName = (typeof(T)).Name;
            switch (typeName)
            {
                case "IUserRepository":
                    return (T)(object)new SqlUserRepository();
                case "ICdsRepository":
                    return (T)(object)new SqlCdsRepository();
                default:
                    throw new ArgumentException($"Failed to inject '{typeName}'");
            }
        }
    }
}
