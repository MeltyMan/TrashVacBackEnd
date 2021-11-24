using System;

namespace TrashVacBackEnd.Core.Repository
{
    public interface ILogRepository
    {
        long WriteToDoorAccessLog(string rfId, string doorId, Guid userId, bool accessStatus);
    }
}