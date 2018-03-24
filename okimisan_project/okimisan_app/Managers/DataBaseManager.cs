using okimisan_app.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okimisan_app.Managers
{
    public class DataBaseManager
    {
        const string ClientTable = "roll_client";
        private static DataBaseManager _instance;
        public static DataBaseManager getInstance()
        {
            if (_instance == null)
                _instance = new DataBaseManager();
            return _instance;
        }
        private OleDbConnection conn;
        private OleDbCommand oleDbCmd = new OleDbCommand();
        private String connParam;
        private DataBaseManager()
        {
            connParam = ConfigurationManager.AppSettings["DBConnect"] + (new FileInfo(ConfigurationManager.AppSettings["DBName"])).FullName;
            conn = new OleDbConnection(connParam);
        }

        public void updateClients()
        {
            Logic.Logic.execute(l => l.clients.allClients = getClients());
        }
        
        public List<Client> getClients()
        {
            List<Client> clients = new List<Client>();
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(string.Format("select * from {0}", ClientTable), connParam);
            OleDbCommandBuilder cBuilder = new OleDbCommandBuilder(dAdapter);

            DataTable dataTable = new DataTable();
            dAdapter.Fill(dataTable);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Client client = new Client();
                client.id = int.Parse(dataTable.Rows[i][0].ToString());
                client.uid = dataTable.Rows[i][1].ToString();
                client.id_cafe = int.Parse(dataTable.Rows[i][2].ToString().Equals(string.Empty) ? "0" : dataTable.Rows[i][2].ToString());
                client.name = dataTable.Rows[i][3].ToString();
                client.phone = dataTable.Rows[i][4].ToString();
                client.street = dataTable.Rows[i][5].ToString();
                client.house = dataTable.Rows[i][6].ToString();
                client.build = dataTable.Rows[i][7].ToString();
                client.gateway = dataTable.Rows[i][8].ToString();
                client.floor = dataTable.Rows[i][9].ToString();
                client.room = dataTable.Rows[i][10].ToString();
                client.intercom = dataTable.Rows[i][11].ToString();
                client.discount = int.Parse(dataTable.Rows[i][12].ToString().Equals(string.Empty) ? "0" : dataTable.Rows[i][12].ToString());
                client.rate = dataTable.Rows[i][13].ToString();
                client.orders = int.Parse(dataTable.Rows[i][14].ToString().Equals(string.Empty) ? "0" : dataTable.Rows[i][14].ToString());
                client.blocked = !dataTable.Rows[i][15].ToString().Equals("0");
                client.deleted = !dataTable.Rows[i][16].ToString().Equals("0");
                client.street2 = dataTable.Rows[i][17].ToString();
                client.house2 = dataTable.Rows[i][18].ToString();
                client.build2 = dataTable.Rows[i][19].ToString();
                client.gateway2 = dataTable.Rows[i][20].ToString();
                client.floor2 = dataTable.Rows[i][21].ToString();
                client.room2 = dataTable.Rows[i][22].ToString();
                client.intercom2 = dataTable.Rows[i][23].ToString();
                client.more = dataTable.Rows[i][24].ToString();
                client.more2 = dataTable.Rows[i][25].ToString();
                client.last_order = dataTable.Rows[i][26].ToString();
                client.uploaded = !dataTable.Rows[i][27].ToString().Equals("0");
                clients.Add(client);
            }

            return clients;
        }   

        public void saveClient(Logic.Logic logic, Client client)
        {
            if (logic.clients.allClients.Where(x => x.id == client.id).Count()>0)
            {
                updateClient(client);
            }
            else
            {
                createClient(client);
            }

            updateClients();
        }

        public void updateClient(Client client)
        {
            //editindb
            #region connectbuilder
            string sql = string.Format("UPDATE {0} SET", ClientTable);
            sql = string.Format("{0}{1}{2}{3}", sql, " name=\"", client.name, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " phone=\"", client.phone, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " street=\"", client.street, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " house=\"", client.house, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " build=\"", client.build, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " gateway=", client.gateway.Equals(string.Empty) ? "null" : client.gateway, ",");
            sql = string.Format("{0}{1}{2}{3}", sql, " floor=", client.floor.Equals(string.Empty) ? "null" : client.floor, ",");
            sql = string.Format("{0}{1}{2}{3}", sql, " room=", client.room.Equals(string.Empty) ? "null" : client.room, ",");
            sql = string.Format("{0}{1}{2}{3}", sql, " intercom=\"", client.intercom, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " discount=", client.discount, ",");
            sql = string.Format("{0}{1}{2}{3}", sql, " last_order=", client.last_order.Equals(string.Empty) ? "null" : "\"" + client.last_order + "\"", ", ");
            sql = string.Format("{0}{1}{2}{3}", sql, " orders=", client.orders, ",");
            sql = string.Format("{0}{1}{2}{3}", sql, " blocked=", client.blocked, ",");
            sql = string.Format("{0}{1}{2}{3}", sql, " deleted=", client.deleted, ",");
            sql = string.Format("{0}{1}{2}{3}", sql, " street2=\"", client.street2, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " house2=\"", client.house2, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " build2=\"", client.build2, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " gateway2=", client.gateway2.Equals(string.Empty) ? "null" : client.gateway2, ",");
            sql = string.Format("{0}{1}{2}{3}", sql, " floor2=", client.floor.Equals(string.Empty) ? "null" : client.floor, ",");
            sql = string.Format("{0}{1}{2}{3}", sql, " room2=", client.room2.Equals(string.Empty) ? "null" : client.room2, ",");
            sql = string.Format("{0}{1}{2}{3}", sql, " intercom2=\"", client.intercom2, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " more=\"", client.more, "\",");
            sql = sql.Remove(sql.Length - 1, 1);
            sql = string.Format("{0}{1}{2}", sql, " WHERE ID=", client.id);
            #endregion connectbuilder
            OleDbConnection conn = null;
            try
            {
                conn = new OleDbConnection(connParam);
                conn.Open();

                OleDbCommand cmd =
                    new OleDbCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public void createClient(Client client)
        {
            //addtodb
            using (OleDbConnection connection = new OleDbConnection(connParam))
            {
                string sql = string.Format("insert into {0}(", ClientTable);
                string sql2 = string.Format(" values (");
                #region addingBuilder
                if (!client.name.Equals(string.Empty))
                {
                    sql = string.Format("{0}name, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.name);
                }
                if (!client.phone.Equals(string.Empty))
                {
                    sql = string.Format("{0}phone, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.phone);
                }
                if (!client.street.Equals(string.Empty))
                {
                    sql = string.Format("{0}street, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.street);
                }
                if (!client.house.Equals(string.Empty))
                {
                    sql = string.Format("{0}house, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.house);
                }
                if (!client.build.Equals(string.Empty))
                {
                    sql = string.Format("{0}build, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.build);
                }
                if (!client.gateway.Equals(string.Empty))
                {
                    sql = string.Format("{0}gateway, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.gateway);
                }
                if (!client.floor.Equals(string.Empty))
                {
                    sql = string.Format("{0}floor, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.floor);
                }
                if (!client.room.Equals(string.Empty))
                {
                    sql = string.Format("{0}room, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.room);
                }
                if (!client.intercom.Equals(string.Empty))
                {
                    sql = string.Format("{0}intercom, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.intercom);
                }
                if (!client.street2.Equals(string.Empty))
                {
                    sql = string.Format("{0}street2, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.street2);
                }
                if (!client.house2.Equals(string.Empty))
                {
                    sql = string.Format("{0}house2, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.house2);
                }
                if (!client.build2.Equals(string.Empty))
                {
                    sql = string.Format("{0}build2, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.build2);
                }
                if (!client.gateway2.Equals(string.Empty))
                {
                    sql = string.Format("{0}gateway2, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.gateway2);
                }
                if (!client.floor2.Equals(string.Empty))
                {
                    sql = string.Format("{0}floor2, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.floor2);
                }
                if (!client.room2.Equals(string.Empty))
                {
                    sql = string.Format("{0}room2, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.room2);
                }
                if (!client.intercom2.Equals(string.Empty))
                {
                    sql = string.Format("{0}intercom2, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.intercom2);
                }
                if (!client.more.Equals(string.Empty))
                {
                    sql = string.Format("{0}more, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.more);
                }
                if (!client.discount.Equals(string.Empty))
                {
                    sql = string.Format("{0}discount, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, client.discount);
                }
                if (!client.orders.Equals(string.Empty))
                {
                    sql = string.Format("{0}orders, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, 0);
                }
                if (!client.blocked.Equals(string.Empty))
                {
                    sql = string.Format("{0}blocked, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, 0);
                }
                if (!client.deleted.Equals(string.Empty))
                {
                    sql = string.Format("{0}deleted, ", sql);
                    sql2 = string.Format("{0}\"{1}\", ", sql2, 0);
                }
                #endregion addingBuilder
                sql = string.Format("{0}{1}{2}{3}", sql.Remove(sql.Length - 2, 2), ")", sql2.Remove(sql2.Length - 2, 2), ")");
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        //Orders
        const string OrderTable = "roll_client";
        public List<Order> getOrders() {
            List<Order> orders = new List<Order>();
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(string.Format("select * from {0}", OrderTable), connParam);
            OleDbCommandBuilder cBuilder = new OleDbCommandBuilder(dAdapter);

            DataTable dataTable = new DataTable();
            dAdapter.Fill(dataTable);
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                Order order = new Order();
                order.id = int.Parse(dataTable.Rows[i][0].ToString());
                order.moment = dataTable.Rows[i][1].ToString();
                order.id_client = int.Parse(dataTable.Rows[i][2].ToString());
                order.id_user = int.Parse(dataTable.Rows[i][3].ToString());
                order.discount = int.Parse(dataTable.Rows[i][4].ToString());
                order.total = int.Parse(dataTable.Rows[i][5].ToString());
                order.content = dataTable.Rows[i][6].ToString();
                order.nightcost = dataTable.Rows[i][7].ToString() == "0" ?  false: true;
                order.carcost = int.Parse(dataTable.Rows[i][8].ToString());
                order.cashback = int.Parse(dataTable.Rows[i][9].ToString());
                order.needcall = dataTable.Rows[i][10].ToString() == "0" ? false : true;
                order.printed = int.Parse(dataTable.Rows[i][11].ToString());
                order.confirmed = dataTable.Rows[i][12].ToString() == "0" ? false : true;
                order.accepted = dataTable.Rows[i][13].ToString() == "0" ? false : true;
                order.deleted = dataTable.Rows[i][14].ToString() == "0" ? false : true;
                //is_address_2
                order.ordernum = int.Parse(dataTable.Rows[i][16].ToString());
                order.totalmax = int.Parse(dataTable.Rows[i][17].ToString());
                order.reason = dataTable.Rows[i][18].ToString();
                order.is_cafe = int.Parse(dataTable.Rows[i][19].ToString());
                order.id_table = int.Parse(dataTable.Rows[i][20].ToString());
                order.num = int.Parse(dataTable.Rows[i][21].ToString());
                order.persons = int.Parse(dataTable.Rows[i][22].ToString());
                order.content = dataTable.Rows[i][23].ToString();

                orders.Add(order);
            }

            return orders;
        }

        public void updateOrder(Order order)
        {
            //editindb
            #region connectbuilder
            string sql = string.Format("UPDATE {0} SET", OrderTable);
            sql = string.Format("{0}{1}{2}{3}", sql, " id_client=\"", order.id_client, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " id_user=\"", order.id_user, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " moment=\"", order.moment, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " discount=\"", order.discount, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " total=\"", order.total, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " content=\"", order.content, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " nightcost=\"", order.nightcost, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " carcost=\"", order.carcost, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " cashback=\"", order.cashback, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " needcall=\"", order.needcall, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " printed=\"", order.printed, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " confirmed=\"", order.confirmed, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " accepted=\"", order.accepted, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " deleted=\"", order.deleted, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " is_address2=\"", order.is_address2, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " ordernum=\"", order.ordernum, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " totalmax=\"", order.totalmax, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " reason=\"", order.reason, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " is_cafe=\"", order.is_cafe, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " id_table=\"", order.id_table, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " num=\"", order.num, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " persons=\"", order.persons, "\",");
            sql = string.Format("{0}{1}{2}{3}", sql, " username=\"", order.username, "\",");
            sql = sql.Remove(sql.Length - 1, 1);
            sql = string.Format("{0}{1}{2}", sql, " WHERE ID=", order.id);
            #endregion connectbuilder
            OleDbConnection conn = null;
            try
            {
                conn = new OleDbConnection(connParam);
                conn.Open();

                OleDbCommand cmd =
                    new OleDbCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        public void updateOrders()
        {
            Logic.Logic.execute(l => l.orders.allOrders = getOrders());
        }

        public void createOrder(Order order)
        {
            
        }

        public void saveOrder(Logic.Logic logic, Order order)
        {
            if (logic.orders.allOrders.Where(x => x.id == order.id).Count() > 0)
            {
                updateOrder(order);
            }
            else
            {
                createOrder(order);
            }

            updateOrders();
        }
    }
}