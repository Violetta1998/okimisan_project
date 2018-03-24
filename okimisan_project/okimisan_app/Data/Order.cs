using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okimisan_app.Data
{
    public class Order
    {
        public int id;
        public string moment;
        public int id_client;
        public int id_user;
        public int discount;
        public int total;
        public string content;
        public bool nightcost;
        public int carcost;
        public int cashback;
        public bool needcall;
        public int printed;
        public bool confirmed;
        public bool accepted;
        public bool deleted;
        public string is_address2;
        public int ordernum;
        public int totalmax;
        public string reason;
        public int is_cafe;
        public int id_table;
        public int num;
        public int persons;
        public string username;
        //public int coins;
        //public int is_happy_time;
        //public int printed_cafe;
        //public int uploaded;
        //public int uid;
        //public int id_www_or;
    }
}
