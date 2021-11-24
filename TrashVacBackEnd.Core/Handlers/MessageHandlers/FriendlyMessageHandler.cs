using System;
using System.Collections.Generic;
using System.Text;

namespace TrashVacBackEnd.Core.Handlers.MessageHandlers
{
    public class FriendlyMessageHandler : IMessageTypeHandler
    {
        public string GetResponse(string message)
        {
            return $"Hej! Vad trevligt!";
        }
    }
}
