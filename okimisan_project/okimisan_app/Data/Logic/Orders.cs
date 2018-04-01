using okimisan_app.Data;
using okimisan_app.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okimisan_app.Logic
{
    public class Orders
    {
        public Orders()
        {
            orders = new Order[0];
            selectedOrder = null;
            editMode = true;
            allOrders = DataBaseManager.getInstance().getOrders();
            allOrderItems = DataBaseManager.getInstance().getOrderItems();
            allOrderLogs = DataBaseManager.getInstance().getOrderLogs();
            allOrderItemAddins = DataBaseManager.getInstance().getOrderItemAddins();
            currentPage = 1;
            isFilterHide = false;
            isInfoHide = false;
        }

        public Order[] orders;
        public Order selectedOrder;
        public bool editMode;
        public List<Order> allOrders;
        public List<OrderItem> allOrderItems;
        public List<OrderLog> allOrderLogs;
        public List<OrderItemAddin> allOrderItemAddins;
        public int currentPage;
        public bool isFilterHide;
        public bool isInfoHide;
    }
}
