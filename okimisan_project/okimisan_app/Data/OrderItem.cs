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
    }
}
