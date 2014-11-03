using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE.Admin
{
    public partial class SignalR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                ListBox1.Items.Add("Items " + i.ToString());
                DropDownList1.Items.Add("Items " + i.ToString());
            }
        }
    }
}