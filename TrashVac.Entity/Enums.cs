using System;

namespace TrashVac.Entity
{
    public class Enums
    {
        public enum UserLevel
        {
            Undefined = 0,
            Standard = 50,
            Admin = 100
        }

        public enum PersistUserTagRelationStatus
        {
            Success = 0, 
            UserNotFound = 1, 
            InvalidDto = 2
        }

        public enum MessageType
        {
            Undefined = 0, 
            Friendly = 1, 
            Sarcastic = 2, 
            Funny = 3
        }

        public enum WeightActionStatusCode
        {
            Undefined = 0,
            Initialized = 1, 
            Complete = 128,
            Failed = 256
        }
    }
}
