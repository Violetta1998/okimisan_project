using okimisan_app.Assets;
using okimisan_app.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okimisan_app.Logic
{
    public class Auth
    {
        public delegate void OnUnAuth();
        public delegate void OnAuth();

        public List<Data.User> usersFromDB;
        public bool isAuth = false;
        public UserType userType = UserType.None;
        public string password;
        public string name;

        public OnUnAuth onUnAuth = () => { };
        public OnAuth onAuth = () => { };

        public Auth()
        {
            usersFromDB = DataBaseManager.getInstance().getUsers();
        }

        public void UnAuth()
        {
            isAuth = false;
            password = string.Empty;
            name = string.Empty;
            userType = UserType.None;
            onUnAuth();
        }


        public void LogIn()
        {
            try
            {
                isAuth = true;
                User currentUser = AuthManager.getInstance().getUser(userType, userType == UserType.Waiter ? name : password);
                if (currentUser == null)
                    throw new Exception("Пользователь не найден");
          
                onAuth();
            }
            catch(Exception ex)
            {
                isAuth = false;
                throw ex;
            }
        }
    }
}
