using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using HiQ.NetStandard.Util.Data;
using TrashVac.Entity;

namespace TrashVacBackEnd.Core.Extensions
{
    public static class ParameterExtensions
    {

        private const string PARAM_NAME_RESULT = "Result";
        private const string PARAM_NAME_USERID = "@UserId";
        private const string PARAM_NAME_RFID = "@RfId";
        private const string PARAM_NAME_DOORID = "@DoorId";
        private const string PARAM_NAME_DELETED = "@Deleted";
        private const string PARAM_NAME_SEARCH_STRING = "@SearchString";
        private const string PARAM_NAME_ACCESS_STATUS = "@AccessStatus";
        private const string PARAM_NAME_LOG_ID = "@LogId";
        private const string PARAM_NAME_ACTION_ID = "@ActionId";
        private const string PARAM_NAME_WEIGHT_POINT = "@WeightPoint";
        private const string PARAM_NAME_WEIGHT_DATA = "@WeightData";
        private const string PARAM_NAME_ACTION_STATUS_CODE = "@ActionStatusCode";
        private const string PARAM_NAME_TOTAL_WEIGHT = "@TotalWeight";
        #region Add


        public static void AddWeightPoint(this SqlParameters p, byte point,
            ParameterDirection direction = ParameterDirection.Input)
        {
            p.AddTinyInt(PARAM_NAME_WEIGHT_POINT, point, direction);
        }

        public static void AddWeightData(this SqlParameters p, double weightData,
            ParameterDirection direction = ParameterDirection.Input)
        {
            p.AddFloat(PARAM_NAME_WEIGHT_DATA, weightData, direction);
        }

        public static void AddActionStatusCode(this SqlParameters p, Enums.WeightActionStatusCode statusCode,
            ParameterDirection direction = ParameterDirection.Input)
        {
            p.AddInt(PARAM_NAME_ACTION_STATUS_CODE, (int)statusCode, direction);
        }

        public static void AddTotalWeight(this SqlParameters p, double totalWeight,
            ParameterDirection direction = ParameterDirection.Input)
        {
            p.AddFloat(PARAM_NAME_TOTAL_WEIGHT, totalWeight, direction);
        }

        public static void AddTotalWeightAsNull(this SqlParameters p)
        {
            var parameter = new SqlParameter(PARAM_NAME_TOTAL_WEIGHT, SqlDbType.Float)
                { Direction = ParameterDirection.Input, Value = DBNull.Value };
            p.Add(parameter);
        }
        public static void AddActionId(this SqlParameters p, Guid actionId,
            ParameterDirection direction = ParameterDirection.Input)
        {
            p.AddUniqueIdentifier(PARAM_NAME_ACTION_ID, actionId, direction);
        }

        public static void AddActionIdAsOutput(this SqlParameters p)
        {
            p.AddActionId(Guid.Empty, ParameterDirection.Output);
        }
        public static void AddAccessStatus(this SqlParameters p, bool accessStatus,
            ParameterDirection direction = ParameterDirection.Input)
        {
            p.AddBoolean(PARAM_NAME_ACCESS_STATUS, accessStatus, direction);
        }

        public static void AddLogIdAsOutput(this SqlParameters p)
        {
            p.AddBigInt(PARAM_NAME_LOG_ID, 0, ParameterDirection.Output);
        }

        public static void AddSearchString(this SqlParameters p, string searchString,
            ParameterDirection direction = ParameterDirection.Input)
        {
            p.AddNVarChar(PARAM_NAME_SEARCH_STRING, 255, searchString, direction);
        }

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

        public static long GetLogId(this SqlParameters p)
        {
            return p.GetLong(PARAM_NAME_LOG_ID);
        }

        public static Guid GetActionId(this SqlParameters p)
        {
            return p.GetGuid(PARAM_NAME_ACTION_ID);
        }
        #endregion



    }
}
