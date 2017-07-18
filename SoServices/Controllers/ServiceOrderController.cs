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
using SoServices.Data;
using SoServices.Hepler;
using SoServices.Models;

namespace SoServices.Controllers
{
    public class ServiceOrderController : ApiController
    {
        ServiceOrderDbEntities dbContext;

        [HttpGet]
        public List<UserServiceOrderDetail> GetUserServiceOrderDetails(int userId)
        {

            using (dbContext = new ServiceOrderDbEntities())
            {
                return dbContext.UserServiceOrderDetails.Where(x => x.UserId == userId).ToList();
            }
        }

        [HttpGet]
        public List<UserServiceOrderDetail> GetDealerServiceOrderDetails(int dealerId)
        {

            using (dbContext = new ServiceOrderDbEntities())
            {
                return dbContext.UserServiceOrderDetails.Where(x => x.DealerId == dealerId).ToList();
            }
        }

        [HttpGet]
        public List<ServicesProvided> GetBasicServicesProvided(int dealerId)
        {

            using (dbContext = new ServiceOrderDbEntities())
            {
                return dbContext.ServicesProvideds.ToList();
            }
        }

        [HttpPost]
        public Result CreateServiceOrder()
        {
            bool success = true;
            string message = string.Empty;
            Task<string> response = Request.Content.ReadAsStringAsync();
            var soDetails = new JavaScriptSerializer().Deserialize<UserServiceOrderDetail>(response.Result).TrimObject();
            try
            {
                using (dbContext = new ServiceOrderDbEntities())
                {
                    //Check for active So's
                    var soActiveDetails =
                        dbContext.UserServiceOrderDetails.FirstOrDefault(x => x.UserId == soDetails.UserId
                                                                              && x.VehicleId == soDetails.VehicleId &&
                                                                              x.Status == 1);
                    if (soActiveDetails != null)
                    {
                        success = false;
                        message = "You already have an active Service order";
                    }
                    else
                    {
                        dbContext.UserServiceOrderDetails.Add(soDetails);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                success = false;
                message = "Error while creating service order";
            }
            return new Result(success, message);
        }
    }
}