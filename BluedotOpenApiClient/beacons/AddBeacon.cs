﻿using System;
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
* Add beacon client demonstrates adding a beacon to to an existing the customer's account using net http web api library
* 
*/

namespace BluedotPublicApiClient.beaconclient
{
    public class BDAddBeacon
    {

        private static String bdApplicationApiKey = "dc99ae20-9192-11e5-8721-0646bf53d23f"; //This apiKey is generated when you create an application
        private static String bdCustomerApiKey    = "7cd1ea80-d40e-11e4-84cb-b8ca3a6b879d"; //This key is generated by Bluedot Point Access UI when your account is created
        private static String bdRestUrl           = "https://api.bluedotinnovation.com/1/beacons";


        public void create()
        {
            WebRequestHandler handler = new WebRequestHandler();
            X509Certificate2 certificate = new X509Certificate2();
            handler.ClientCertificates.Add(certificate);
            HttpClient httpRestClient = new HttpClient(handler);

            //specify to use TLS 1.2 as default connection
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            httpRestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent jsonBeaconContent = new StringContent(getJsonBeacon());
            jsonBeaconContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage serverResponse = httpRestClient.PostAsync(new Uri(bdRestUrl), jsonBeaconContent).Result;
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

        /*JSON to create a beacon*/
        private static String getJsonBeacon()
        {
              return "{" +
                "\"security\": {" +
                 "\"apiKey\":" + "\"" + bdApplicationApiKey + "\"," +
                 "\"customerApiKey\":" + "\"" + bdCustomerApiKey + "\" " +
             "}," +
             "\"content\": {" +
                "\"beacon\": {" +
                    "\"name\": \"Bluedot building \"," +
                    "\"proximityUUUID\": \"f7826da6-4fa2-4e98-8024-bc5b71e0893e\"," +
                    "\"longitude\": \"123.34455\"," +
                    "\"latitude\": \"47.777888\"," +
                    "\"type\": \"Both\"," +
                    "\"major\": 12," +
                    "\"minor\": 13," +
                    "\"txPower\": -77," +
                    "\"macAddress\": \"01:17:C5:31:84:21\"," +
                    "\"description\": \"Sample Description\"" +
                "}" +
            "}" +
          "}";
        }
    }
}
