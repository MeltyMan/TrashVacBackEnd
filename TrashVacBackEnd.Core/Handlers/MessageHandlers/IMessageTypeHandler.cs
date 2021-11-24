namespace TrashVacBackEnd.Core.Handlers.MessageHandlers
{
    public interface IMessageTypeHandler
    {
        string GetResponse(string message);
    }
}