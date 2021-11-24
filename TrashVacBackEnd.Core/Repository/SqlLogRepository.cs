using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HiQ.NetStandard.Util.Data;
using TrashVacBackEnd.Core.Extensions;

namespace TrashVacBackEnd.Core.Repository
{
    public class SqlLogRepository : ADbRepositoryBase, ILogRepository
    {
        public long WriteToDoorAccessLog(string rfId, string doorId, Guid userId, bool accessStatus)
        {
            var parameters = new SqlParameters();
            parameters.AddRfId(rfId);
            parameters.AddDoorId(doorId);
            parameters.AddUserId(userId);
            parameters.AddAccessStatus(accessStatus);
            parameters.AddLogIdAsOutput();
            DbAccess.ExecuteNonQuery("dbo.sDoorAccessLog_Add", ref parameters, CommandType.StoredProcedure);
            return parameters.GetLogId();
        }
    }
}
