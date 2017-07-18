using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Xsl;
using SoServices.Data;
using SoServices.Hepler;
using SoServices.Models;

namespace SoServices.Controllers
{
    public class UserDetailsController : ApiController
    {
        ServiceOrderDbEntities dbContext;

        [HttpGet]
        public Result ValidateUser(string login, string password)
        {
            var success = true;
            string message = string.Empty;

            using (dbContext = new ServiceOrderDbEntities())
            {
                UserDetail userInfo;
                if (IsValidEmail(login))
                    userInfo = dbContext.UserDetails.FirstOrDefault(x => x.EmailAddress == login);
                else
                    userInfo = dbContext.UserDetails.FirstOrDefault(x => x.UserName == login).TrimObject();
                if (userInfo == null)
                {
                    success = false;
                    message = "Username or Emailaddress not recognized";
                }
                else
                {
                    if (userInfo.Password != password)
                    {
                        success = false;
                        message = "Invalid Password";
                    }
                }

            }
            return new Result(success, message);
        }

        [HttpPost]
        public Result RegisterUser()
        {
            var success = true;
            string message = string.Empty;
            try
            {
                Task<string> response = Request.Content.ReadAsStringAsync();
                var userDetail = response.Result;
                var userInfo = new JavaScriptSerializer().Deserialize<UserDetail>(userDetail).TrimObject();
                var res = validateUserInfo(userInfo);
                if (!res.success)
                    return res;
                using (dbContext = new ServiceOrderDbEntities())
                {
                    dbContext.UserDetails.Add(userInfo);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                success = false;
                message = "Error while registering user";
            }
            return new Result(success, message);
        }

        #region PrivateMethods

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private Result validateUserInfo(UserDetail user)
        {
            var success = true;
            string message = string.Empty;
            using (dbContext = new ServiceOrderDbEntities())
            {
                if (dbContext.UserDetails.FirstOrDefault(x => x.EmailAddress == user.EmailAddress) != null)
                {
                    success = false;
                    message = "Email Address already registered";
                }

                else if (dbContext.UserDetails.FirstOrDefault(x => x.UserName == user.UserName) != null)
                {
                    success = false;
                    message = "UserName already registered";
                }

                else if (dbContext.UserDetails.FirstOrDefault(x => x.CellPhone == user.CellPhone) != null)
                {
                    success = false;
                    message = "Number already registered";
                }
            }
            return new Result(success, message);

        }

        #endregion

    }
}