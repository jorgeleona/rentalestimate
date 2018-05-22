using System;
using System.Net;
using System.Xml;
using rentalestimate.dataaccess.Interfaces;
using rentalestimate.model.Users;
using rentalestimate.model.Zillow;

namespace rentalestimate.dataaccess.Repositories
{
	public class ZillowRepository : IZillowRepository
    {
       
		private readonly string _zwsid;

		public ZillowRepository(){

			_zwsid = "X1-ZWz18lqk3392q3_6frtk";
		}

		public SearchResult GetSearchResults(UserInformation userInformation){

			string address = userInformation.Address.Replace(' ','+');
			string citystatezip = $"{userInformation.City}+{userInformation.StateCode}+{userInformation.ZipCode}";
			String url = $"http://www.zillow.com/webservice/GetSearchResults.htm?zws-id={_zwsid}&address={address}&citystatezip={citystatezip}";

			SearchResult result = new SearchResult();

			try
            {                
                HttpWebRequest Request = (System.Net.HttpWebRequest)HttpWebRequest.Create(url);
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                
				Decimal innerValue = 0;
				string innerText = string.Empty;

                XmlDocument xml = new XmlDocument();
                xml.Load(Response.GetResponseStream());
                XmlElement root = xml.DocumentElement;
                                
				XmlNode xmlNode = root.SelectSingleNode("//response/results/result[1]/zestimate/amount");
				innerText = xmlNode != null ? xmlNode.InnerText : "0";
				result.zestimateAmount = Decimal.TryParse(innerText, out innerValue) ? innerValue : 0;

				xmlNode = root.SelectSingleNode("//response/results/result[1]/rentzestimate/valuationRange/low");
				innerText = xmlNode != null ? xmlNode.InnerText : "0";
				result.rentZestimateValuationLow = Decimal.TryParse(innerText, out innerValue) ? innerValue : 0;
                
				xmlNode = root.SelectSingleNode("//response/results/result[1]/rentzestimate/valuationRange/high");
				innerText = xmlNode != null ? xmlNode.InnerText : "0";
				result.renrZestimateValuationHigh = Decimal.TryParse(innerText, out innerValue) ? innerValue : 0;

			}
			catch(Exception exc){
				//TODO: Add log
				string error = exc.Message;
			}

			return result;
		}
    }
}
