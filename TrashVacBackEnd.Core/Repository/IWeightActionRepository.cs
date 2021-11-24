using System;
using TrashVac.Entity;

namespace TrashVacBackEnd.Core.Repository
{
    public interface IWeightActionRepository
    {
        Guid Initialize(Guid userId, string doorId);
        void UpdateStatus(Guid actionId, Enums.WeightActionStatusCode statusCode, double totalWeight = -1);
        long AddWeightData(Guid actionId, byte point, double weightData);
    }
}