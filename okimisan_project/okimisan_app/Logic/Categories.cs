using okimisan_app.Data;
using System.Collections.Generic;

namespace okimisan_app.Logic
{
    public class Categories
    {
        public Categories()
        {
            category = new Category[0];
            selectedOrder = null;
            editMode = true;
          //  allCategory = DataBaseManager.getInstance().allCategories();
            currentPage = 1;
            isFilterHide = false;
            isInfoHide = false;
        }
        public Category[] category;
        public Category selectedOrder;
        public bool editMode;
        public List<Category> allCategory;
        public int currentPage;
        public bool isFilterHide;
        public bool isInfoHide;
    }
}
