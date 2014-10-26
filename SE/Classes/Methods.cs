using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace SE.Classes
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

        public static string UploadFile(FileUpload File, String FileType)
        {
            String path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/");
            string Message = "";
            string[] VideoExt = new string[4] { ".avi", ".wmv", ".mp4", ".mpg" };
            string[] AudioExt = new string[4] { ".mp3", ".wav", ".m4a", ".mwa" };
            string[] ImageExt = new string[4] { ".jpg", ".png", ".gif", ".bmp" };
            bool VideoGood, AudioGood, ImageGood;
            VideoGood = AudioGood = ImageGood = false;

            if (FileType == "Video")
            {
                foreach (string ext in VideoExt)
                {
                    if(ext ==  System.IO.Path.GetExtension(File.FileName))
                    {
                        VideoGood = true;
                        break;
                    }
                }

                if (!VideoGood)
                    Message = "Extension " + System.IO.Path.GetExtension(File.FileName) + " is invalid. Valid video extensions: " + String.Join(", ", VideoExt);
            }

            if (FileType == "Audio")
            {
                foreach (string ext in AudioExt)
                {
                    if (ext == System.IO.Path.GetExtension(File.FileName))
                    {
                        AudioGood = true;
                        break;
                    }
                }

                if(!AudioGood)
                    Message = "Extension " + System.IO.Path.GetExtension(File.FileName) + " is invalid. Valid audio extensions: " + String.Join(", ", AudioExt);
            }

            if (FileType == "Image")
            {
                foreach (string ext in ImageExt)
                {
                    if (ext == System.IO.Path.GetExtension(File.FileName))
                    {
                        ImageGood = true;
                        break;
                    }
                }  

                if(!ImageGood)
                    Message = "Extension " + System.IO.Path.GetExtension(File.FileName) + " is invalid. Valid image extensions: " + String.Join(", ", ImageExt);
            }


            /*try
            {
                File.PostedFile.SaveAs(path
                    + File.FileName);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }*/

            return Message;
        }
    }
}