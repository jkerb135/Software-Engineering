﻿using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Mail;
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
                if (fileType == "Image")
                {
                    var resize = new Bitmap(file.PostedFile.InputStream);
                    var newImg = ResizeImage(resize, 225, 200);
                    newImg.Save(path + file.FileName);
                }
                else
                {
                    file.PostedFile.SaveAs(path + file.FileName);
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }

        public static string SendEmail(string sender, string receiver, string body)
        {
            var mailClient = new SmtpClient();
            var error = "";
            //This object stores the authentication values      
            var basicCredential =
                new System.Net.NetworkCredential("ipawsteamb@gmail.com", "holymoneychildren");
            mailClient.Host = "smtp.gmail.com";
            mailClient.Port = 587;
            mailClient.EnableSsl = true;
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = basicCredential;


            var message = new MailMessage();

            var fromAddress = new MailAddress(sender, "Backend Team B");
            
            message.From = fromAddress;   
            message.To.Add(receiver);
            message.IsBodyHtml = true;
            message.Body = body;

            try
            {
                mailClient.Send(message);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return error;
        }
        public static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}