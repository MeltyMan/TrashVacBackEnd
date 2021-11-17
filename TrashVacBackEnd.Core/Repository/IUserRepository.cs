using System;
using System.Collections.Generic;
using TrashVac.Entity;

namespace TrashVacBackEnd.Core.Repository
{
    public interface IUserRepository
    {
        IList<User> GetUserList();
        Guid CreateUser(UserFull user);
        bool TryLogin(string userName, string password, out UserAuthenticated user);
        bool ValidateToken(string token, out User user);
        User GetUserById(Guid userId);
    }
}
