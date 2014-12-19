﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace BluedotPublicApiClient.zoneclient
{
    public class CreateApplicationAction
    {
        private static String bdCustomerApiKey    = "bc199c80-5441-11e4-b7bb-a0481cdc3311"; //This key is generated by Bluedot Access UI when you register
        private static String bdApplicationApiKey = "d3161e80-38d1-11e4-b039-bc305bf60831"; //This apiKey is generated when you create an application
        private static String bdRestUrl           = "http://localhost:3000/1/fence/create";
        private static String bdZoneId            = "24d9a245-2087-421b-9972-2af2ee0970f1"; //This is the id of the zone being updated. This can be fetched by calling zones/getAll API


        public void add()
        {
            postToService(getJsonApplicationAction());
        }


        private void postToService(String json)
        {
            HttpClient httpRestClient = new HttpClient();
            httpRestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent jsonContent = new StringContent(json);
            jsonContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage serverResponse = httpRestClient.PostAsync(new Uri(bdRestUrl), jsonContent).Result;
            if (serverResponse.IsSuccessStatusCode)
            {
                var result = serverResponse.Content.ReadAsStringAsync().Result;
                Console.WriteLine("{0}", result);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)serverResponse.StatusCode, serverResponse.Content.ReadAsStringAsync().Result);
            }
        }


        /*JSON Format for an application action*/
        private static String getJsonApplicationAction()
        {
            String applicationActionJson =
                 "{" +
                    "\"security\": {" +
                        "\"apiKey\":" + "\"" + bdApplicationApiKey + "\"," +
                        "\"customerApiKey\":" + "\"" + bdCustomerApiKey + "\"" +
                    "}," +
                    "\"content\": {" +
                        "\"zone\": {" +
                            "\"zoneId\":" + "\"" + bdZoneId + "\"," +
                            "\"actions\": {" +
                                "\"customActions\": [" +
                                    "{" +
                                        "\"name\": \"A Custom Application action\"" +
                                    "}" +
                                "]" +
                            "}" +
                        "}" +
                    "}" +
                "}";
            return applicationActionJson;
        }

    }
}
