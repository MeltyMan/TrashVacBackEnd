using System.Collections.Generic;
using TrashVac.Entity;

namespace TrashVacBackEnd.Core.Repository
{
    public interface IDoorRepository
    {
        IList<DoorWithAccess> GetDoorListWithAccess(string rfId);
        bool PersistDoorAccess(string rfId, string doorId, bool deleted);
        bool PersistDoorAccess(string rfId, IList<DoorWithAccess> doorAccessList);
    }
}