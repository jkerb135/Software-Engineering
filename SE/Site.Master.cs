using System;
using System.Linq;
using System.Web.Security;
using SE.Models;
using SE.Classes;
using System.Globalization;

namespace SE
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public string getProfilePic { set; get; }
        public string getOtherProfilePic { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            getProfilePic = ResolveUrl("/Images/default.png");
            if (IsPostBack) return;
            var UserName = System.Web.HttpContext.Current.User.Identity.Name;
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            var formatUsername = textInfo.ToTitleCase(UserName);
            username.Text = " " + formatUsername + " ";
            if (!Roles.IsUserInRole(UserName, "Manager"))
            {
                CreateUserMenu.Visible = false;
                ReportsMenu.Visible = false;
            }
            else
            {
                CategoriesMenu.Visible = false;
                Li1.Visible = false;
                ProfilePic.Visible = false;
                RequestsMenu.Visible = false;
            }
            getPictureFromDb(UserName);
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            var membershipUser = Membership.GetUser();
            if (membershipUser.Comment != "Verified")
            {
                membershipUser.Comment = null;
                Membership.UpdateUser(membershipUser);
            }
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void getPictureFromDb(string username)
        {
            using (var db = new ipawsTeamBEntities())
            {
                var profile = db.Profiles.FirstOrDefault(find => find.Name == username);
                if (profile == null)
                {
                    ProfilePicture.ImageUrl = ResolveUrl("/Images/default.png");
                    getProfilePic = ResolveUrl("/Images/default.png");
                }
                else
                {
                    ProfilePicture.ImageUrl = profile.Picture.ToString();
                    getProfilePic = profile.Picture.ToString();
                }
            }
        }
        public string getOtherPictureFromDb(string username)
        {
            string url;
            using (ipawsTeamBEntities db = new ipawsTeamBEntities())
            {
                var profile = db.Profiles.FirstOrDefault(find => find.Name == username);
                if (profile == null)
                {
                    url = ResolveUrl("/Images/default.png");
                }
                else
                {
                    url = profile.Picture.ToString();
                }
            }
            return url;
        }

        protected void UploadFile_Click(object sender, EventArgs e)
        {
            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            if (ProfileUpload.HasFile)
            {
                string Message = "";
                using (ipawsTeamBEntities db = new ipawsTeamBEntities())
                {
                    var exists = db.Profiles.FirstOrDefault(find => find.Name == userName);
                    Message = Methods.UploadFile(ProfileUpload, "Image");
                    if (exists != null)
                    {
                        exists.Picture = "~/Uploads/" + ProfileUpload.FileName;
                    }
                    else { 
                        if (Message == "")
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
                getPictureFromDb(userName);
            }
        }
    }
}