using System;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;

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

        public static string UploadFile(FileUpload file, String fileType)
        {
            var path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/");
            var message = "";
            string[] videoExt = { ".avi", ".wmv", ".mp4", ".mpg" };
            string[] audioExt = { ".mp3", ".wav", ".m4a", ".mwa" };
            string[] imageExt = { ".jpg", ".png", ".gif", ".bmp" };
            bool audioGood, imageGood;
            var videoGood = audioGood = imageGood = false;

            if (fileType == "Video")
            { 
                if (videoExt.Any(ext => ext ==  System.IO.Path.GetExtension(file.FileName)))
                {
                    videoGood = true;
                }

                if (!videoGood)
                    message = "Extension " + System.IO.Path.GetExtension(file.FileName) + " is invalid. Valid video extensions: " + String.Join(", ", videoExt);
            }

            if (fileType == "Audio")
            {
                if (audioExt.Any(ext => ext == System.IO.Path.GetExtension(file.FileName)))
                {
                    audioGood = true;
                }

                if(!audioGood)
                    message = "Extension " + System.IO.Path.GetExtension(file.FileName) + " is invalid. Valid audio extensions: " + String.Join(", ", audioExt);
            }

            if (fileType == "Image")
            {
                if (imageExt.Any(ext => ext == System.IO.Path.GetExtension(file.FileName)))
                {
                    imageGood = true;
                }

                if(!imageGood)
                    message = "Extension " + System.IO.Path.GetExtension(file.FileName) + " is invalid. Valid image extensions: " + String.Join(", ", imageExt);
            }

            if (!videoGood && !audioGood && !imageGood) return message;
            try
            {
                file.PostedFile.SaveAs(path
                                       + file.FileName);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }
    }
}