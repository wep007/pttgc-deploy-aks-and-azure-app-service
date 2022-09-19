using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PTTGCSampleApp.Models;

namespace PTTGCSampleApp.Repository
{
    public interface IUserRepository
    {
        IEnumerable<UserProfile> GetUsers();

        UserProfile GetUserByID(int UserID);

        UserProfile InsertUser(UserProfile User);
    }
}
