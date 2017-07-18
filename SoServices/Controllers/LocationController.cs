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
    public class LocationController : ApiController
    {
        ServiceOrderDbEntities dbContext;

        [HttpGet]
        public List<Location> GetAllLocations()
        {
            using (dbContext = new ServiceOrderDbEntities())
                return dbContext.Locations.ToList();
        }

        [HttpGet]
        public Location GetLocationDetails(int locationId)
        {
            using (dbContext = new ServiceOrderDbEntities())
                return dbContext.Locations.FirstOrDefault(x => x.LocationId == locationId);
        }
    }
}