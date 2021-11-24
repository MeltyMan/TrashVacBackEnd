using System;
using System.Collections.Generic;
using System.Text;

namespace TrashVacBackEnd.Core.Handlers.MessageHandlers
{
    public class SarcasticMessageHandler : IMessageTypeHandler
    {
        public string GetResponse(string message)
        {
            return $"{message}.... Vad snackar du om?";
        }
    }
}
