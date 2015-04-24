/*
Author			: Josh Kerbaugh\Daniel Talley
Creation Date	: 9/4/2014
Date Finalized	: 12/6/2014
Course			: CSC354 - Software Engineering
Professor Name	: Dr. Tan
Assignment # 	: Team B - iPAWS
Filename		: Site.Master.cs
Purpose			: This file declares and instantiates the look and feel of the site between the two views(Supervisor,Manager). It sets the general layout of each users GUI.
*/

using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using SE.Classes;
using SE.Models;

namespace SE
{
    public partial class Site : MasterPage
    {
        public string GetProfilePic { set; get; }
        public string GetOtherProfilePic { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            GetProfilePic = ResolveUrl("/Images/default.png");
            if (IsPostBack) return;
            var userName = HttpContext.Current.User.Identity.Name;
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            var formatUsername = textInfo.ToTitleCase(userName);
            username.Text = " " + formatUsername + " ";
            if (!Roles.IsUserInRole(userName, "Manager"))
            {
                ReportsMenu.Visible = false;
                SupervisorHome.Visible = true;
                ManagerHome.Visible = false;
            }
            else
            {
                SupervisorHome.Visible = false;
                ManagerHome.Visible = true;
                CategoriesMenu.Visible = false;
                Li1.Visible = false;
                ProfilePic.Visible = false;
                RequestsMenu.Visible = false;
                Requests1.Visible = Requests2.Visible = false;

            }
            GetPictureFromDb(userName);
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            var membershipUser = Membership.GetUser();
            if (membershipUser != null && membershipUser.Comment != "Verified")
            {
                membershipUser.Comment = null;
                Membership.UpdateUser(membershipUser);
            }
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void GetPictureFromDb(string username)
        {
            using (var db = new ipawsTeamBEntities())
            {
                var profile = db.Profiles.FirstOrDefault(find => find.Name == username);
                if (profile == null)
                {
                    ProfilePicture.ImageUrl = ResolveUrl("/Images/default.png");
                    GetProfilePic = ResolveUrl("/Images/default.png");
                }
                else
                {
                    ProfilePicture.ImageUrl = profile.Picture;
                    GetProfilePic = profile.Picture;
                }
            }
        }
        public string GetOtherPictureFromDb(string username)
        {
            string url;
            using (var db = new ipawsTeamBEntities())
            {
                var profile = db.Profiles.FirstOrDefault(find => find.Name == username);
                url = profile == null ? ResolveUrl("/Images/default.png") : profile.Picture;
            }
            return url;
        }

        protected void UploadFile_Click(object sender, EventArgs e)
        {
            var userName = HttpContext.Current.User.Identity.Name;
            if (!ProfileUpload.HasFile) return;
            using (var db = new ipawsTeamBEntities())
            {
                var exists = db.Profiles.FirstOrDefault(find => find.Name == userName);
                var message = Methods.UploadFile(ProfileUpload, "Image");
                if (exists != null)
                {
                    exists.Picture = "~/Uploads/" + ProfileUpload.FileName;
                }
                else { 
                    if (message == "")
                    {
                        var newProfile = new Profile
                        {
                            Name = userName,
                            Picture = "~/Uploads/" + ProfileUpload.FileName,
                        };
                        db.Profiles.Add(newProfile);
                    }
                }
                db.SaveChanges();
            }
            GetPictureFromDb(userName);
        }
        
    }
}