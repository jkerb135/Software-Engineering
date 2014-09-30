using SE.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SE
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getActiveUsers();
        }
        protected void getActiveUsers()
        {
            activeUserList.DataSource = Member.CustomGetActiveUsers();
            activeUserList.DataBind();
        }
    }
}
