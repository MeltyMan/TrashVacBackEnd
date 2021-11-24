using System;
using System.Collections.Generic;
using System.Text;
using TrashVacBackEnd.Core.Handlers;
using TrashVacBackEnd.Core.Repository;

namespace TrashVacBackEnd.Core
{
    public class Injector
    {
        public static T GetInstance<T>()
        {
            var typeName = typeof(T).Name;

            switch (typeName)
            {
                case "IDoorRepository":
                    return (T)(object)new SqlDoorRepository();
                case "ILogRepository":
                    return (T)(object)new SqlLogRepository();
                case "IRfIdRepository":
                    return (T)(object)new SqlRfIdRepository();
                case "IUserRepository":
                    return (T)(object)new SqlUserRepository();
                case "IMessageHandler":
                    return (T)(object)new MessageHandler();
                case "IWeightActionRepository":
                    return (T)(object)new SqlWeightActionRepository();
                default:
                    throw new ArgumentException($"Failed to create instance of '{typeName}'.");
                    
            }

        }
    }
}
