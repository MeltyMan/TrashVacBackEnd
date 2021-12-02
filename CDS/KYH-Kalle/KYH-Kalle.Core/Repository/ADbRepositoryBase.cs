using System;
using System.Collections.Generic;
using System.Text;
using HiQ.NetStandard.Util.Data;

namespace KYH_Kalle.Core.Repository
{
    public abstract class ADbRepositoryBase
    {
        private readonly SqlDbAccess _dbAccess;

        protected ADbRepositoryBase()
        {
            _dbAccess = new SqlDbAccess(ServiceProvider.Current.Configuration.ConnectionStrings.KYHConnectionString);
        }

        protected SqlDbAccess DbAccess => _dbAccess;
    }
}
