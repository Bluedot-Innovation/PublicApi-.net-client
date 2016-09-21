using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
using System.Security.Cryptography.X509Certificates;

/**
 * @author Bluedot Innovation
 * Copyright (c) 2016 Bluedot Innovation. All rights reserved.
 * Add Beacon to Zone client demonstrates adding a beacon to an existing zone to an existing application
 */

namespace BluedotPublicApiClient.beaconclient
{
    public class  BDAddBeacontoZone
    {
        private static String bdApplicationApiKey = "a251fbd2-5035-4887-896d-0b56abb4102d"; // This apiKey is generated when you create an application a
        private static String bdCustomerApiKey    = "dc67d0e0-397b-11e5-9931-0646bf53d23f"; // This key is generated by Bluedot Point Access UI when your account is created
        private static String bdZoneId            = "9bf7ff72-cb3e-4ef2-b1b6-b8aa5a119256"; // This is the id of the zone being updated. This can be fetched by calling GET Zones API
		private static String bdBeaconId          = "b7db6f18-6557-4903-b0a7-b35b96d54fec"; // This is the id of the beacon being updated and retrived from Bluedot UI.
     	private static String bdRestUrl           = "https://api.bluedotinnovation.com/1/zones/beacons";
         
         public void create()
        {
            postToService(getjsonAddBeacontoZone());
        }
         private void postToService(String json)
         {
             WebRequestHandler handler = new WebRequestHandler();
             X509Certificate2 certificate = new X509Certificate2();
             handler.ClientCertificates.Add(certificate);
             HttpClient httpRestClient = new HttpClient(handler);

             //specify to use TLS 1.2 as default connection
             System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            httpRestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent jsonAddBeacontoZoneContent = new StringContent(json); 
            jsonAddBeacontoZoneContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage serverResponse = httpRestClient.PostAsync(new Uri(bdRestUrl), jsonAddBeacontoZoneContent).Result;
            if (serverResponse.IsSuccessStatusCode)
            {
                var result = serverResponse.Content.ReadAsStringAsync().Result;

                Console.WriteLine("{0}", result.ToString());
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)serverResponse.StatusCode, serverResponse.Content.ReadAsStringAsync().Result);
            }
        }
        
        private static String getjsonAddBeacontoZone()
        {
            return "{" +
                 "\"security\": {" +
                            "\"apiKey\":" + "\"" + bdApplicationApiKey + "\"," +
                            "\"customerApiKey\":" + "\"" + bdCustomerApiKey + "\"" +
                "}," +
               "\"content\": {" +
                     "\"zone\": {" +
                        "\"zoneId\":" + "\"" + bdZoneId + "\" "+
                        "\"beacons\": [" +
				            "{" +
					            "\"beaconId\": " +" \"" + bdBeaconId + "\" " +
					    	    "\"proximity\": 1" +
					        "}"+
					    "]"+
				    "}"+
			    "}"+
		    "}";
        }
    }
}