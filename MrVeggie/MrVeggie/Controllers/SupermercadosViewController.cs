using Microsoft.AspNetCore.Mvc;

namespace MrVeggie.Controllers
{
    [Route("[controller]/[action]")]
    public class SupermercadosViewController : Controller
    {

        public ActionResult ShowLocalizacao()
        {
            Location[] localizacoes = new Location[] { new Location(41.557568, -8.406558), new Location(40.557568, -8.406558), new Location(42.557568, -8.406558) };
            return View(localizacoes);
        }

        public class Location
        {
            public double latitude;
            public double longitude;

            public Location(double latitude, double longitude)
            {
                this.latitude = latitude;
                this.longitude = longitude;
            }
        }
    }

}

