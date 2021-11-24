using System.Collections.Generic;
using HiQ.NetStandard.Util.Data;
using System.Data;
using TrashVac.Entity;
using TrashVacBackEnd.Core.Extensions;

namespace TrashVacBackEnd.Core.Repository
{
    public class SqlRfIdRepository : ADbRepositoryBase, IRfIdRepository
    {
        private readonly IUserRepository _userRepository;

        public SqlRfIdRepository()
        {
            _userRepository = Injector.GetInstance<IUserRepository>();
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

        public IList<RfIdTag> GetList()
        {
            var result = new List<RfIdTag>();
           
            var dr = DbAccess.ExecuteReader("dbo.sRfIdTag_GetList", CommandType.StoredProcedure);

            while (dr.Read())
            {
                result.Add(new RfIdTag() { RfId = dr.GetString(0), Description = dr.GetString(1) });
            }

            DbAccess.DisposeReader(ref dr);


            return result;

        }
    }
}
