using System;
using System.Collections.Generic;
using System.Text;
using HiQ.NetStandard.Util.Data;

namespace TrashVacBackEnd.Core.Repository
{
    public abstract class ADbRepositoryBase
    {
        private readonly SqlDbAccess _sqlDbAccess;

        protected ADbRepositoryBase()
        {
            _sqlDbAccess =
                new SqlDbAccess(ServiceProvider.Current.Configuration.ConnectionStrings.TrashVacDbConnectionString);
        }

        protected SqlDbAccess DbAccess
        {
            get { return _sqlDbAccess; }
        }

    }
}
