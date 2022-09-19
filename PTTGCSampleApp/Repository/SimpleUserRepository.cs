using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PTTGCSampleApp.Models;

namespace PTTGCSampleApp.Repository
{

    public class SimpleUserRepository : IUserRepository
    {
        private readonly Dictionary<int, UserProfile> _store;

        public SimpleUserRepository()
        {
            _store = new Dictionary<int, UserProfile>();
        }

        public UserProfile InsertUser(UserProfile User)
        {
            // Ignore given id
            int generatedID = _store.Count + 1;
            User.Id = generatedID;
            _store.Add(generatedID, User);

            return User;
        }

        public IEnumerable<UserProfile> GetUsers()
        {
            return _store.Values.AsEnumerable();
        }

        public UserProfile GetUserByID(int UserID)
        {
            if (_store.ContainsKey(UserID))
            {
                return _store[UserID];
            }

            return null;
        }

    }
}
