using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FactoryPattern
{
    public partial class FactoryHome : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack==false)
            {
                FactoryTypeList.Items.Add("1");
                FactoryTypeList.Items.Add("2");
                FactoryTypeList.Items.Add("3");
            }
        }

        protected void FactoryTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string receivedItem = FactoryTypeList.SelectedValue;

            Ingrediants makeProduct = null;

            makeProduct = Factory.GetProduct(receivedItem);

            string result = makeProduct.Connection("First", "Second");

            CurrentProductItem.Text = result;

        }
    }
}