using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HiQ.NetStandard.Util.Data;
using TrashVacBackEnd.Core.Entity;
using TrashVacBackEnd.Core.Extensions;

namespace TrashVacBackEnd.Core.Repository
{
    public class SqlRfIdRepository : ADbRepositoryBase, IRfIdRepository
    {
        private readonly IUserRepository _userRepository;

        public SqlRfIdRepository()
        {
            _userRepository = new SqlUserRepository();
        }

        public ValidateRfIdResponse ValidateRfIdAccess(string rfId, string doorId)
        {
            var parameters = new SqlParameters();
            parameters.AddRfId(rfId);
            parameters.AddDoorId(doorId);
            parameters.AddResult();
            parameters.AddUserIdAsOutput();

            DbAccess.ExecuteNonQuery("[dbo].[sRfId_ValidateAccess]", ref parameters, CommandType.StoredProcedure);

            if (parameters.GetResult())
            {
                return new ValidateRfIdResponse()
                    { IsValid = true, RfId = rfId, User = _userRepository.GetUserById(parameters.GetUserId()) };
            }
            else
            {
                return new ValidateRfIdResponse() { IsValid = false, RfId = rfId };
            }


        }

    }
}
