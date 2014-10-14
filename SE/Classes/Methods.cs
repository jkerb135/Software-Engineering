using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace SE.Cla sses
{
    public static class Methods
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString();
        }

        public static void AddBlankToDropDownList(DropDownList drp)
        {
            // Add default blank list item
            drp.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            drp.SelectedIndex = 0;
        }

        public static string UploadFile(FileUpload File)
        {
            String path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/");
            string Message = "";

            try
            {
                File.PostedFile.SaveAs(path
                    + File.FileName);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return Message;
        }
    }
}