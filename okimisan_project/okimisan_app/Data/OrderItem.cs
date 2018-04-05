using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okimisan_app.Data
{
    public class OrderItem
    {
        public int id;
        public int order_id;
        public string name;
        public int id_catalog;
        public int size;
        public int price;
        public int count;
        public int addin_price;
        public int addin_count;
        public bool bonus;
        public long uid;
        public int coins;

        public string getFullName(Logic.Logic logic)
        {
            string fullName = string.Format("{0} шт., {1}({2}), ", size, name, id_catalog);

            var addins = logic.orders.allOrderItemAddins.Where(x => x.id_order_item == id);

            foreach (var addin in addins)
            {
                fullName = string.Format("{0}{1}{2}, ", fullName, addin.remove ? "-" : "+", logic.products.allAddins.Where(x => x.id == addin.id_addin).First());
            }

            fullName = string.Format("{0}{1} руб.", fullName, price * count);

            return fullName;
        }
    }
}
