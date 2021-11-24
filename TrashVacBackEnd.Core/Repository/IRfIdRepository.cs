using System.Collections.Generic;
using TrashVac.Entity;

namespace TrashVacBackEnd.Core.Repository
{
    public interface IRfIdRepository
    {
        ValidateRfIdResponse ValidateRfIdAccess(string rfId, string doorId);
        IList<RfIdTag> GetList();
    }
}