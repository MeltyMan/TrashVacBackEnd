using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HiQ.NetStandard.Util.Data;

namespace TrashVacBackEnd.Core.Extensions
{
    public static class ParameterExtensions
    {

        private const string PARAM_NAME_RESULT = "Result";
        private const string PARAM_NAME_USERID = "@UserId";
        private const string PARAM_NAME_RFID = "@RfId";
        private const string PARAM_NAME_DOORID = "@DoorId";
        private const string PARAM_NAME_DELETED = "@Deleted";

        #region Add

        public static void AddDeleted(this SqlParameters p, bool deleted,
            ParameterDirection direction = ParameterDirection.Input)
        {
            p.AddBoolean(PARAM_NAME_DELETED, deleted, direction);
        }
        public static void AddDoorId(this SqlParameters p, string doorId,
            ParameterDirection direction = ParameterDirection.Input)
        {
            p.AddVarChar(PARAM_NAME_DOORID, 50, doorId, direction);
        }

        public static void AddRfId(this SqlParameters p, string rfId,
            ParameterDirection direction = ParameterDirection.Input)
        {
            p.AddVarChar(PARAM_NAME_RFID, 50, rfId, direction);
        }
        public static void AddResult(this SqlParameters p)
        {
            p.AddBoolean(PARAM_NAME_RESULT, false, ParameterDirection.Output);
        }

        public static void AddUserId(this SqlParameters p, Guid userId,
            ParameterDirection direction = ParameterDirection.Input)
        {
            p.AddUniqueIdentifier(PARAM_NAME_USERID, userId, direction);
        }

        public static void AddUserIdAsOutput(this SqlParameters p)
        {
            AddUserId(p, Guid.Empty, ParameterDirection.Output);
        }
        #endregion

        #region Get

        public static bool GetResult(this SqlParameters p)
        {
            return p.GetBool(PARAM_NAME_RESULT);
        }

        public static Guid GetUserId(this SqlParameters p)
        {
            return p.GetGuid(PARAM_NAME_USERID);
        }
        #endregion



    }
}
