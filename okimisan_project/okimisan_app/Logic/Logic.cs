﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okimisan_app.Logic
{
    public delegate void LogicUpdateHandler(Logic logic);
    public delegate void LogicExecutor(Logic logic);

    public class Logic
    {
        static Logic _instance = new Logic();
        static List<LogicUpdateHandler> _updateHandlers = new List<LogicUpdateHandler>();

        public static void execute(LogicExecutor executor)
        {          
            executor(_instance);

            LogicUpdateHandler[] hArr = new LogicUpdateHandler[_updateHandlers.Count];
            _updateHandlers.CopyTo(hArr);
            foreach (var handler in hArr)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    handler(_instance);
                });
            }            
        }
        
        public static void onLogicUpdate(LogicUpdateHandler handler)
        {
            _updateHandlers.Add(handler);
        }

        public static void removeHandler(LogicUpdateHandler handler)
        {
            _updateHandlers.Remove(handler);
        }

        public Auth auth;
        public General general;
        public Clients clients;
        public Orders orders;
        public Categories categories;
        public Products products;

        public Logic()
        {
            auth = new Auth();
            general = new General();
            clients = new Clients();
            orders = new Orders();
            categories = new Categories();
            products = new Products();

            auth.onAuth = () => {
                if (auth.isAuth)
                    general.currentPage = General.PAGES.Main1;
            };

            auth.onUnAuth = () =>
            {
                Console.WriteLine("UnAuth");
            };
        }

    }
}
