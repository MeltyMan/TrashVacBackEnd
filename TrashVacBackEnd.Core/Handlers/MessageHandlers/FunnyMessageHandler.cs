using System;
using System.Collections.Generic;
using System.Text;

namespace TrashVacBackEnd.Core.Handlers.MessageHandlers
{
    public class FunnyMessageHandler : IMessageTypeHandler
    {
        public string GetResponse(string message)
        {
            return $"Jag gillar inte honung, för det har sådan bismak!";
        }
    }
}
