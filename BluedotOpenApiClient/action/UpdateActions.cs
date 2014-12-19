﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading.Tasks;

/**
 * @author Bluedot Innovation
 * Update Action REST client demonstrating adding a actions to an existing zone using net http web api libraries
 */

namespace BluedotPublicApiClient.zoneclient
{
    public class UpdateActions
    {
        private static String bdCustomerApiKey    = "bc199c80-5441-11e4-b7bb-a0481cdc3311"; //This key is generated by Bluedot Access UI when you register
        private static String bdApplicationApiKey = "d3161e80-38d1-11e4-b039-bc305bf60831"; //This apiKey is generated when you create an application
        private static String bdRestUrl           = "https://api.bluedotinnovation.com/1/actions";
        private static String bdZoneId            = "24d9a245-2087-421b-9972-2af2ee0970f1"; //This is the id of the zone being updated. This can be fetched by calling zones/getAll API
        private static String urlActionId         = "6de96865-f3c8-42cf-bdc2-38027272770a"; //This is the id of the action being updated. This can be fetch by calling zones/get?id=yourzoneid
        private static String messageActionId     = "6de96865-f3c8-42cf-bdc2-38027272770a"; //This is the id of the action being updated. This can be fetch by calling zones/get?id=yourzoneid

        public void updateMessageAction()
        {
            HttpClient httpRestClient = new HttpClient();
            httpRestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent jsonContent = new StringContent(getJsonMessageAction());
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

        public void updateURLAction()
        {
            HttpClient httpRestClient = new HttpClient();
            httpRestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent jsonContent = new StringContent(getJsonUrlAction());
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

        private static String getJsonMessageAction()
        {
            String messageActionJson =
                 "{" +
                    "\"security\": {" +
                        "\"apiKey\":" + "\"" + bdApplicationApiKey + "\"," +
                        "\"customerApiKey\":" + "\"" + bdCustomerApiKey + "\"" +
                    "}," +
                    "\"content\": {" +
                        "\"zone\": {" +
                            "\"zoneId\":" + "\"" + bdZoneId + "\"," +
                            "\"actions\": {" +
                                "\"messageActions\": [" +
                                    "{" +
                                      "\"actionId\":" + "\"" + messageActionId + "\"," +
                                        "\"name\": \"Welcome to MCG\"," +
                                        "\"title\": \"MCG Welcome Message Updated\"," +
                                        "\"message\": \"Welcome to MCG Updated Message\"" +
                                    "}" +
                                "]" +
                            "}" +
                        "}" +
                    "}" +
                "}";
            return messageActionJson;
        }

        private static String getJsonUrlAction()
        {

            String urlActionJson =
                 "{" +
                    "\"security\": {" +
                        "\"apiKey\":" + "\"" + bdApplicationApiKey + "\"," +
                        "\"customerApiKey\":" + "\"" + bdCustomerApiKey + "\"" +
                    "}," +
                    "\"content\": {" +
                        "\"zone\": {" +
                            "\"zoneId\":" + "\"" + bdZoneId + "\"," +
                            "\"actions\": {" +
                                "\"urlActions\": [" +
                                    "{" +
                                        "\"actionId\":" + "\"" + urlActionId + "\"," +
                                        "\"name\": \"Bluedot URL Updated\"," +
                                        "\"url\": \"http://www.bluedotinnovation.com\"" +
                                    "}" +
                                "]" +
                            "}" +
                        "}" +
                    "}" +
                "}";
            return urlActionJson;
        }
    }
}
