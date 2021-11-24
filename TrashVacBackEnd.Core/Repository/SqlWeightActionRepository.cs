using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HiQ.NetStandard.Util.Data;
using TrashVac.Entity;
using TrashVacBackEnd.Core.Extensions;

namespace TrashVacBackEnd.Core.Repository
{
    public class SqlWeightActionRepository : ADbRepositoryBase, IWeightActionRepository
    {
        public Guid Initialize(Guid userId, string doorId)
        {

            var parameters = new SqlParameters();
            parameters.AddUserId(userId);
            parameters.AddDoorId(doorId);
            parameters.AddActionIdAsOutput();
            DbAccess.ExecuteNonQuery("dbo.sWeightAction_Initialize", ref parameters, CommandType.StoredProcedure);
            return parameters.GetActionId();

        }

        public void UpdateStatus(Guid actionId, Enums.WeightActionStatusCode statusCode, double totalWeight = -1.0)
        {
            var parameters = new SqlParameters();
            parameters.AddActionId(actionId);
            parameters.AddActionStatusCode(statusCode);
            if (statusCode == Enums.WeightActionStatusCode.Complete && totalWeight > 0.0)
            {
                parameters.AddTotalWeight(totalWeight);
            }
            else
            {
                parameters.AddTotalWeightAsNull();
            }

            DbAccess.ExecuteNonQuery("dbo.sWeightAction_UpdateStatus", ref parameters, CommandType.StoredProcedure);

        }

        public long AddWeightData(Guid actionId, byte point, double weightData)
        {
            var parameters = new SqlParameters();
            parameters.AddActionId(actionId);
            parameters.AddWeightPoint(point);
            parameters.AddWeightData(weightData);
            parameters.AddLogIdAsOutput();
            DbAccess.ExecuteNonQuery("dbo.sWeightActionData_Add", ref parameters, CommandType.StoredProcedure);
            return parameters.GetLogId();
        }
    }
}
