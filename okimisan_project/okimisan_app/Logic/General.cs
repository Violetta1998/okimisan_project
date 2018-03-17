using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace okimisan_app.Logic
{
    public class General
    {
        private Stack<PAGES> history = new Stack<PAGES>();

        private PAGES _currentPage = PAGES.Auth;
        private MODAL_PAGES _currentModalPage = MODAL_PAGES.None;
        private Dictionary<PAGES, Page> _pages = new Dictionary<PAGES, Page>();
        private Dictionary<MODAL_PAGES, Page> _modalPages = new Dictionary<MODAL_PAGES, Page>();

        public bool mainMenuIsVisible = false;

        public General()
        {

        }

        public void InitializeScreens()
        {
            _pages.Add(PAGES.Auth, new Screens.Auth());
            _pages.Add(PAGES.Main1, new Screens.Main1());
            _pages.Add(PAGES.None, null);
            _pages.Add(PAGES.CreateOrder, new Screens.CreateOrder());
            _pages.Add(PAGES.ClientsList, new Screens.ClientsList());
            _pages.Add(PAGES.OrderList, new Screens.OrderList());

            _modalPages.Add(MODAL_PAGES.None, null);
            _modalPages.Add(MODAL_PAGES.ClientEdit, new Screens.EditClient());
        }

        public PAGES currentPage
        {
            get { return _currentPage; }
            set
            {
                history.Push(_currentPage);
                _currentPage = value;
            }
        }

        public MODAL_PAGES currentModalPage
        {
            get { return _currentModalPage; }
            set
            {
                _currentModalPage = value;
            }
        }

        public Page getPage(PAGES page)
        {
            return _pages[page];
        }

        public Page getModalPage(MODAL_PAGES page)
        {
            return _modalPages[page];
        }

        public void Back()
        {
            if (history.Count > 0)
                _currentPage = history.Pop();
        }

        public enum PAGES
        {
            None, Main1, Auth, CreateOrder, ClientsList, OrderList
        }

        public enum MODAL_PAGES
        {
            None, ClientEdit
        }
    }
}
