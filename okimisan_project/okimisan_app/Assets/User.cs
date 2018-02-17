using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okimisan_app.Assets
{
    public class User
    {
        public User(UserType userType, string name, string password)
        {
            this.userType = userType;
            this.name = name;
            this.password = password;
        }
        public UserType userType;
        public string name;
        public string password;
    }
}
