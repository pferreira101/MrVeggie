using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;

namespace MrVeggie.Controllers {

    [Route("[controller]/[action]")]
    public class SupermercadosViewController : Controller {

        public ActionResult ShowLocalizacao() {
            return View(ProcessQuery());
        }


        public List<MarketLocation> ProcessQuery() {
            double SearchLatitude = 41.480687;
            double SearchLongitude = -8.527346;
            double Radius = 10; // km  
            int maxResults = 5;
            string bingMapsKey = "Alvpuc-Z8ROrtuOcQZdVD1iaINzybihaHRnSHYWL8jwdEjVrXRj843L8ayxchoj7";
            string requestUrl = string.Format(CultureInfo.InvariantCulture, "http://spatial.virtualearth.net/REST/v1/data/c2ae584bbccc4916a0acf75d1e6947b4/NavteqEU/NavteqPOIs" +
                "?spatialFilter=nearby({0},{1},{2})&$filter=EntityTypeID Eq 5400&$top={3}&key={4}", SearchLatitude, SearchLongitude, Radius, maxResults, bingMapsKey);

            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(response.GetResponseStream());

            return ProcessEntityElements(xmlDoc);
        }

        private List<MarketLocation> ProcessEntityElements(XmlDocument response) {
            List<MarketLocation> results = new List<MarketLocation>();
            XmlNodeList entryElements = response.GetElementsByTagName("entry");

            for (int i = 0; i <= entryElements.Count - 1; i++) {
                XmlElement element = (XmlElement)entryElements[i];
                XmlElement contentElement = (XmlElement)element.GetElementsByTagName("content")[0];
                XmlElement propElement = (XmlElement)contentElement.GetElementsByTagName("m:properties")[0];
                XmlNode nameElement = propElement.GetElementsByTagName("d:Name")[0];
                if (nameElement == null) throw new Exception("Name not found");

                XmlNode latElement = propElement.GetElementsByTagName("d:Latitude")[0];
                if (latElement == null) throw new Exception("Latitude not found");

                XmlNode longElement = propElement.GetElementsByTagName("d:Longitude")[0];
                if (longElement == null) throw new Exception("Longitude not found");

                string name = nameElement.InnerText;
                string lat = latElement.InnerText.ToString(CultureInfo.InvariantCulture).Replace("−", "-");
                string lon = longElement.InnerText.ToString(CultureInfo.InvariantCulture).Replace("−", "-");

                double latitude = 0;
                Double.TryParse(lat, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out latitude);

                double longitude = 0;
                Double.TryParse(lon, NumberStyles.AllowDecimalPoint| NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out longitude);

                results.Add(new MarketLocation(name, latitude, longitude));
                Console.WriteLine("LOCALIZAÇÃO : {0}, {1}, {2}", name, latitude, longitude);
            }

            return results;
        }

        public class MarketLocation {
            public double latitude;
            public double longitude;
            public string nome;

            public MarketLocation(string nome, double latitude, double longitude) {
                this.nome = nome;
                this.latitude = latitude;
                this.longitude = longitude;
            }
        }
    }

}
