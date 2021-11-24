using System;
using System.Collections.Generic;
using System.Text;
using TrashVac.Entity;
using TrashVacBackEnd.Core.Handlers.MessageHandlers;

namespace TrashVacBackEnd.Core.Handlers
{
    public class MessageHandler : IMessageHandler
    {
        public MessageHandler()
        {
            _messageTypeHandlers = new Dictionary<Enums.MessageType, IMessageTypeHandler>();
            _messageTypeHandlers.Add(Enums.MessageType.Friendly, new FriendlyMessageHandler());
            _messageTypeHandlers.Add(Enums.MessageType.Sarcastic, new SarcasticMessageHandler());
            _messageTypeHandlers.Add(Enums.MessageType.Funny, new FunnyMessageHandler());

        }

        private readonly IDictionary<Enums.MessageType, IMessageTypeHandler> _messageTypeHandlers;


        public bool TryGetResponse(Enums.MessageType messageType, string message, out string response)
        {
            response = string.Empty;

            if (TryGetHandler(messageType, out var handler))
            {
                response = handler.GetResponse(message);
                return true;
            }

            return false;
        }

        private bool TryGetHandler(Enums.MessageType messageType, out IMessageTypeHandler handler)
        {
            handler = null;
            if (_messageTypeHandlers.ContainsKey(messageType))
            {
                return _messageTypeHandlers.TryGetValue(messageType, out handler);
            }

            return false;
        }

    }
}
