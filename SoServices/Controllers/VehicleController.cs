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
    public class VehicleController : ApiController
    {
        ServiceOrderDbEntities dbContext;

        [HttpGet]
        public List<Vehicle> GetUserVehicles(int userId)
        {
            using (dbContext = new ServiceOrderDbEntities())
                return dbContext.Vehicles.Where(x => x.UserId == userId).ToList();


        }

        [HttpGet]
        public Result DeleteVehicle(int vehicleId)
        {
            bool success = true;
            string message = string.Empty;
            try
            {
                using (dbContext = new ServiceOrderDbEntities())
                {
                    var veh = dbContext.Vehicles.FirstOrDefault(x => x.VehicleId == vehicleId);
                    dbContext.Vehicles.Remove(veh);
                }
            }
            catch (Exception)
            {
                success = false;
                message = "Error while deleting vehicle";
            }
            return new Result(success, message);
        }

        [HttpPost]
        public Result AddVehicle()
        {
            bool success = true;
            string message = string.Empty;
            Task<string> response = Request.Content.ReadAsStringAsync();
            var vehicle = new JavaScriptSerializer().Deserialize<Vehicle>(response.Result).TrimObject();
            try
            {
                using (dbContext = new ServiceOrderDbEntities())
                {
                    var vehDetails = dbContext.Vehicles.FirstOrDefault(x => vehicle.VIN != null && x.VIN == vehicle.VIN);
                    if (vehDetails != null)
                    {
                        success = false;
                        message = "Vehicle with this VIN already exists";
                    }
                    else
                    {
                        dbContext.Vehicles.Add(vehicle);
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