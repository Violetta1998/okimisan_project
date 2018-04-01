using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okimisan_app.Data
{
    public class OrderLog
    {
        public int id;
        public int id_order;
        public int id_user;
        public int version;
        public string moment;
        public string action;
        public string name;
        public string value_was;
        public string value_now;
        public int difference;
        public string host;
        public bool uploaded;
    }
}
