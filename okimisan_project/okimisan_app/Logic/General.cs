using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okimisan_app.Logic
{
    public class General
    {
        public List<String> pages = new List<string>();

        private Stack<PAGES> history = new Stack<PAGES>();

        private PAGES _currentPage = PAGES.Auth;

        public bool mainMenuIsVisible = false;

        public PAGES currentPage
        {
            get { return _currentPage; }
            set
            {
                history.Push(_currentPage);
                _currentPage = value;
            }
        }

        public void Back()
        {
            if (history.Count > 0)
                _currentPage = history.Pop();
        }

        public enum PAGES
        {
            Main1, Auth
        }       
    }
}
