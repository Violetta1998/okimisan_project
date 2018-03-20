using okimisan_app.Data;
using okimisan_app.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okimisan_app.Logic
{
    public class Clients
    {
        public Clients()
        {
            clients = new Client[0];
            selectedClient = null;
            editMode = true;
            allClients = DataBaseManager.getInstance().getClients();
            currentPage = 1;
        }

        public Client[] clients;
        public Client selectedClient;
        public bool editMode;
        public List<Client> allClients;
        public int currentPage;
    }
}
