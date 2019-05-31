using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Geolocation;
using Microsoft.AspNetCore.Mvc;

namespace MrVeggie.Controllers
{
    [Route("[controller]/[action]")]
    public class SupermercadosViewController : Controller
    {

        public ActionResult ShowLocalizacao()
        {
            return View(ProcessQuery());
        }



        public List<LatLongLocation> calculaMaisProximo(){
            LatLongLocation[] localizacoes = new LatLongLocation[] { new LatLongLocation(41.857568, -8.206558),  new LatLongLocation(41.531926, -8.447604), new LatLongLocation(41.531928, -8.443604), new LatLongLocation(40.857568, -8.406558), new LatLongLocation(41.530129, -8.447593) };

            LatLongLocation origin = new LatLongLocation(41.529002, -8.445308);
            var results = localizacoes
                           .OrderBy(x => GeoCalculator.GetDistance(origin.latitude, origin.longitude, x.latitude, x.longitude, 1))
                           .Take(3)
                           .ToList();

            return results;
        }


        public List<MarketLocation> ProcessQuery()
        {
            double SearchLatitude = 41.480687;
            double SearchLongitude = -8.527346;
            double Radius = 10; // km  
            int maxResults = 5;
            string bingMapsKey = "Alvpuc-Z8ROrtuOcQZdVD1iaINzybihaHRnSHYWL8jwdEjVrXRj843L8ayxchoj7";
            string requestUrl = string.Format("http://spatial.virtualearth.net/REST/v1/data/c2ae584bbccc4916a0acf75d1e6947b4/NavteqEU/NavteqPOIs" +
                "?spatialFilter=nearby({0},{1},{2})&$filter=EntityTypeID Eq 5400&$top={3}&key={4}", SearchLatitude, SearchLongitude, Radius, maxResults,bingMapsKey);

            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(response.GetResponseStream());
            return ProcessEntityElements(xmlDoc);
        }

        private List<MarketLocation> ProcessEntityElements(XmlDocument response)
        {
            List<MarketLocation> results = new List<MarketLocation>();
            XmlNodeList entryElements = response.GetElementsByTagName("entry");
            for (int i = 0; i <= entryElements.Count - 1; i++)
            {
                XmlElement element = (XmlElement)entryElements[i];
                XmlElement contentElement = (XmlElement)element.GetElementsByTagName(
                  "content")[0];
                XmlElement propElement = (XmlElement)
                  contentElement.GetElementsByTagName("m:properties")[0];
                XmlNode nameElement = propElement.GetElementsByTagName("d:Name")[0];
                if (nameElement == null)
                    throw new Exception("Name not found");
                XmlNode latElement = propElement.GetElementsByTagName("d:Latitude")[0];
                if (latElement == null)
                    throw new Exception("Latitude not found");
                XmlNode longElement = propElement.GetElementsByTagName("d:Longitude")
                  [0];
                if (longElement == null)
                    throw new Exception("Longitude not found");
                string name = nameElement.InnerText;
                Console.WriteLine("******* " + name);
                double latitude = 0;
                Double.TryParse(latElement.InnerText, out latitude);
                double longitude = 0;
                Double.TryParse(longElement.InnerText, out longitude);
                results.Add(new MarketLocation(name,latitude, longitude));
            }
            return results;
        }

        /*public async Task<LatLongLocation> FazRequestAsync() {
            //Create a request.
            var request = new LocationRecogRequest()
            {
                CenterPoint = new Coordinate(41.557568, -8.406558),
                DistanceUnits = DistanceUnitType.Kilometers,
                IncludeEntityTypes = "address",
                Top = 10,
                Radius = 2,
                VerbosePlaceNames = false,
                BingMapsKey = "Alvpuc-Z8ROrtuOcQZdVD1iaINzybihaHRnSHYWL8jwdEjVrXRj843L8ayxchoj7"
            };

            //Process the request by using the ServiceManager.
            var response = await ServiceManager.GetResponseAsync(request);

            if (response != null &&
                response.ResourceSets != null &&
                response.ResourceSets.Length > 0 &&
                response.ResourceSets[0].Resources != null &&
                response.ResourceSets[0].Resources.Length > 0)
            {
                var result = response.ResourceSets[0].Resources[0] as BingMapsRESTToolkit.Location;
                return new LatLongLocation(result.Point.Coordinates[0], result.Point.Coordinates[1]);
            }
            return null;
        }
        */

        /*public static LatLongLocation Geocode(string address)
        {
            string point = "41.557568,-8.406558";
            string key = "Alvpuc-Z8ROrtuOcQZdVD1iaINzybihaHRnSHYWL8jwdEjVrXRj843L8ayxchoj7";
            //string url = "https://dev.virtualearth.net/REST/v1/LocationRecog/41.557568,-8.406558?radius=2&top=10&includeEntityTypes=businessAndPOI&key=Alvpuc-Z8ROrtuOcQZdVD1iaINzybihaHRnSHYWL8jwdEjVrXRj843L8ayxchoj7";
            string url = "http://dev.virtualearth.net/REST/v1/locationrecog/47.609722,-122.333056?key=" + key + "&includeEntityTypes=address&output=json";

            using (var client = new WebClient())
            {
                string response1 = client.DownloadString(url);
                Console.WriteLine("**********\n"+response1);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));
                using (var es = new MemoryStream(Encoding.Unicode.GetBytes(response1)))
                {
                    var response = (ser.ReadObject(es) as Response); //Response is one of the Bing Maps DataContracts
                    var result = response.ResourceSets[0].Resources[0] as BingMapsRESTToolkit.Location;
                    return new LatLongLocation(result.Point.Coordinates[0], result.Point.Coordinates[1]);
                }
            }
        }
        */

        public class LatLongLocation
        {
            public double latitude;
            public double longitude;

            public LatLongLocation(double latitude, double longitude)
            {
                this.latitude = latitude;
                this.longitude = longitude;
            }
        }

        public class MarketLocation
        {
            public double latitude;
            public double longitude;
            public string nome;

            public MarketLocation(string nome, double latitude, double longitude)
            {
                this.nome = nome;
                this.latitude = latitude;
                this.longitude = longitude;
            }
        }
    }

}
