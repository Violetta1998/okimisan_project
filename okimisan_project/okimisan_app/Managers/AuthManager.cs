using okimisan_app.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okimisan_app.Managers
{
    public class AuthManager
    {
        private static AuthManager _instance;

        public static AuthManager getInstance()
        {
            if (_instance == null)
                _instance = new AuthManager();
            return _instance;
        }

        private AuthManager()
        {
            currentUser = null;
            users = new Dictionary<UserType, User[]>();
            users.Add(UserType.Boss, new User[] { new User(UserType.Boss, null, "likeaboss") });
            users.Add(UserType.Chef, new User[] { new User(UserType.Chef, null, "likeachef1"), new User(UserType.Chef, null, "likeachef2") });
            users.Add(UserType.HallManager, new User[] { new User(UserType.HallManager, null, "likeahallmanager1"), new User(UserType.HallManager, null, "likeahallmanager2") });
            users.Add(UserType.Manager, new User[] { new User(UserType.Manager, null, "likeamanager1"), new User(UserType.Manager, null, "likeamanager2") });
        }

        private User currentUser;

        private Dictionary<UserType, User[]> users;        
        
        public User getUser(UserType userType, string additionalInfo)
        {
            if (userType == UserType.Waiter)
                return new User(UserType.Waiter, additionalInfo, null);
            else
            {
                return users[userType].Where(x => x.password.Equals(additionalInfo)).FirstOrDefault();
            }
        }
    }
}
