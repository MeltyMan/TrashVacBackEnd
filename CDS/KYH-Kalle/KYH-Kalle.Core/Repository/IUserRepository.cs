using System;
using System.Collections.Generic;
using KYH_Kalle.Core.Entity;

namespace KYH_Kalle.Core.Repository
{
    public interface IUserRepository
    {
        IList<ApiUserMini> GetUserList();
        bool ValidateToken(string token, out Guid userId);

        Guid CreateUser(ApiUserFull user, out string errorMessage);
        bool TryLogin(string userName, string password, out ApiUser user);
        bool ConnectCustomer(Guid userId, Guid customerId);
        bool UpdateUser(ApiUserFull user, out ApiUserFull updatedUser, out string errorMessage);
        CustomerFull GetUser(Guid userId);
    }
}