using TrashVac.Entity;

namespace TrashVacBackEnd.Core.Handlers
{
    public interface IMessageHandler
    {
        bool TryGetResponse(Enums.MessageType messageType, string message, out string response);
    }
}