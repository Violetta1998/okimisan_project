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

        public string getFullName(Logic.Logic logic, bool bold = false)
        {
            string fullName = string.Format("<b>{0} шт.</b>, {1}({2}), ", size, name, id_catalog);

            var addins = logic.orders.allOrderItemAddins.Where(x => x.id_order_item == id);

            foreach (var addin in addins)
            {
                fullName = string.Format("{0}{1}{2}, ", fullName, addin.remove ? "-" : "+", logic.products.allAddins.Where(x => x.id == addin.id_addin).First());
            }

            fullName = string.Format("{0}<b>{1} руб.</b>", fullName, price * count);

            if (!bold)
            {
                fullName = fullName.Replace("<b>", string.Empty);
                fullName = fullName.Replace("</b>", string.Empty);
            }

            return fullName;
        }
    }
}
