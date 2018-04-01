using okimisan_app.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okimisan_app.Data.Logic
{
    public class Products
    {
        public Products()
        {
            allAddins = DataBaseManager.getInstance().getAddins();
        }

        List<Addin> allAddins;
    }
}
