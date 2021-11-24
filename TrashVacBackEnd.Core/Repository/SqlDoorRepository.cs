using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HiQ.NetStandard.Util.Data;
using TrashVac.Entity;
using TrashVacBackEnd.Core.Extensions;

namespace TrashVacBackEnd.Core.Repository
{
    public class SqlDoorRepository : ADbRepositoryBase, IDoorRepository
    {
        public IList<DoorWithAccess> GetDoorListWithAccess(string rfId)
        {
            var doorList = new List<DoorWithAccess>();
            var parameters = new SqlParameters();
            parameters.AddRfId(rfId);
            var dr = DbAccess.ExecuteReader("dbo.sDoors_GetListWithAccess", ref parameters,
                CommandType.StoredProcedure);
            while (dr.Read())
            {

                doorList.Add(new DoorWithAccess()
                {
                    Id = dr.GetString(0),
                    Description = dr.GetString(1),
                    HasAccess = !dr.IsDBNull(2)
                });
            }

            DbAccess.DisposeReader(ref dr);
            return doorList;
        }

        public bool PersistDoorAccess(string rfId, string doorId, bool deleted)
        {
            var parameters = new SqlParameters();
            parameters.AddRfId(rfId);
            parameters.AddDoorId(doorId);
            parameters.AddDeleted(deleted);
            DbAccess.ExecuteNonQuery("[dbo].[sRfIdDoorAccess_Persist]", ref parameters, CommandType.StoredProcedure);
            return true;
        }

        public bool PersistDoorAccess(string rfId, IList<DoorWithAccess> doorAccessList)
        {
            var result = false;
            foreach (var door in doorAccessList)
            {
                result = PersistDoorAccess(rfId, door.Id, !door.HasAccess);
            }

            return result;
        }
    }
}
