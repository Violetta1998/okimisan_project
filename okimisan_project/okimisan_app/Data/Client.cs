using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okimisan_app.Data
{
    public class Client
    {
        public int id;
        public string uid;
        public int id_cafe;
        public string name;
        public string phone;
        public string street;
        public string house;
        public string build;
        public string gateway;
        public string floor;
        public string room;
        public string intercom;
        public int discount;
        public string rate;
        public int orders;
        public bool blocked;
        public bool deleted;
        public string street2;
        public string house2;
        public string build2;
        public string gateway2;
        public string floor2;
        public string room2;
        public string intercom2;
        public string more;
        public string more2;
        public string last_order;
        public bool uploaded;

        public string getFirstAddress()
        {
            string address = string.Empty;
            if (!street.Equals(string.Empty))
            {
                address += string.Format("{0}, ", street);
            }
            if (!house.Equals(string.Empty))
            {
                address += string.Format("дом {0}, ", house);
            }
            if (!build.Equals(string.Empty))
            {
                address += string.Format("п. {0}, ", build);
            }
            if (!gateway.Equals(string.Empty))
            {
                address += string.Format("в. {0}, ", gateway);
            }
            if (!floor.Equals(string.Empty))
            {
                address += string.Format("этаж {0}, ", floor);
            }
            if (!room.Equals(string.Empty))
            {
                address += string.Format("кв. {0}, ", room);
            }
            if (!intercom.Equals(string.Empty))
            {
                address += string.Format("дом. {0}, ", intercom);
            }

            return address.Substring(0, address.Length > 2 ? address.Length - 2 : address.Length);
        }

        public string getSecondAddress()
        {
            string address = string.Empty;
            if (!street2.Equals(string.Empty))
            {
                address += string.Format("{0}, ", street2);
            }
            if (!house2.Equals(string.Empty))
            {
                address += string.Format("дом {0}, ", house2);
            }
            if (!build2.Equals(string.Empty))
            {
                address += string.Format("п. {0}, ", build2);
            }
            if (!gateway2.Equals(string.Empty))
            {
                address += string.Format("в. {0}, ", gateway2);
            }
            if (!floor2.Equals(string.Empty))
            {
                address += string.Format("этаж {0}, ", floor2);
            }
            if (!room2.Equals(string.Empty))
            {
                address += string.Format("кв. {0}, ", room2);
            }
            if (!intercom2.Equals(string.Empty))
            {
                address += string.Format("дом. {0}, ", intercom2);
            }

            return address.Substring(0, address.Length > 2 ? address.Length - 2 : address.Length);
        }
    }
}
