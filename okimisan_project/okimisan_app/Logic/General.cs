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
        public List<String> pages = new List<string>();

        private Stack<PAGES> history = new Stack<PAGES>();

        private PAGES _currentPage = PAGES.Auth;
        private Dictionary<PAGES, Page> _pages = new Dictionary<PAGES, Page>();

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

        public Page getPage(PAGES page)
        {
            return _pages[page];
        }

        public void Back()
        {
            if (history.Count > 0)
                _currentPage = history.Pop();
        }

        public enum PAGES
        {
            None, Main1, Auth, CreateOrder
        }       
    }
}
