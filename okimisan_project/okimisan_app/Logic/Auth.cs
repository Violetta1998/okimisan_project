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

        public bool isAuth = false;

        public OnUnAuth onUnAuth = () => { };
        public OnAuth onAuth = () => { };

        public void UnAuth()
        {
            isAuth = false;
            onUnAuth();
        }


        public void LogIn()
        {
            try
            {
                isAuth = true;
                onAuth();
            }
            catch
            {
                Console.WriteLine("Error");
            }
        }
    }
}
