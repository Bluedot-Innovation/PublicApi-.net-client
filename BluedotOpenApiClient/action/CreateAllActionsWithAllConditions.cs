﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;

/**
 * @author Bluedot Innovation
 * Copyright (c) 2016 Bluedot Innovation. All rights reserved.
 * Add Action with conditions client shows adding an action with conditions to an existing zone using .net http web api libray
 */

namespace BluedotPublicApiClient.actionclient
{
    public class CreateAllActionsWithAllConditions
    {
        private static String bdCustomerApiKey    = "a6598740-75f5-11e4-86ca-a0481cdc3311"; //This key is generated by Bluedot Point Access UI when your account is created.
        private static String bdApplicationApiKey = "5a9b1b78-3dd7-4f4d-8608-82408f3baf4c"; //This apiKey is generated when you create an application
        private static String bdZoneId            = "b80e50eb-b9f9-4ed5-966e-4b2e39cc0549"; //This is the id of the zone being updated. This can be fetched by calling GET Zones API
        private static String bdRestUrl           = "https://api.bluedotinnovation.com/1/actions";

        private void postToService(String json)
        {
            WebRequestHandler handler = new WebRequestHandler();
            X509Certificate2 certificate = new X509Certificate2();
            handler.ClientCertificates.Add(certificate);
            HttpClient httpRestClient = new HttpClient(handler);

            //specify to use TLS 1.2 as default connection
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

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

        public void addConditionsToUrlAction()
        {
            postToService(getJsonURLActionWithConditions());
        }

        public void addConditionsToMessageAction()
        {
            postToService(getJsonMessageActionWithConditions());
        }

        public void addConditionsToVibrationAction()
        {
            postToService(getJsonVibrationActionWithConditions());
        }

        public void addConditionsToSoundAction()
        {

        }

        private static String getJsonURLActionWithConditions()
        {
            String urlActionWithCondtionsJson =
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
                                 "\"name\": \"Bluedot URL\"," +
                                 "\"url\": \"http://www.bluedotinnovation.com\"" +
                                    "\"conditions\": {" +
                                        "\"percentageCrossed\":" +
                                            "[" +
                                                "{" +
                                                    "\"percentage\": 50," +
                                                    "\"timeoutPeriod\": \"00:05\"" +
                                                "}" +
                                            "]," +
                                        "\"dateRange\": [" +
                                            "{" +
                                                "\"start\": \"01/03/2014\"," +
                                                "\"end\": \"14/12/2014\"" +
                                            "}" +
                                        "]," +
                                        "\"timeActive\": [{" +
                                            "\"from\": {" +
                                                "\"time\": \"06:01\"," +
                                                "\"period\": \"am\" " +
                                            "}," +
                                            "\"to\": {" +
                                                "\"time\": \"11:00\"," +
                                                "\"period\": \"pm\" " +
                                            "}" +
                                        "}]" +
                                    "}" +
                                "}" +
                            "]" +
                          "}" +
                        "}" +
                    "}" +
                "}";

            return urlActionWithCondtionsJson;
        }

        private static String getJsonMessageActionWithConditions()
        {
            String action =
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
                                "\"name\" : \"Date Range My message actions1\"," +
                                "\"title\" : \"This is the title of my message.1\"," +
                                "\"message\" : \"Have a nice day.1\"," +
                                "\"conditions\": {" +                                  
                                    "\"dateRange\": [" +
                                        "{" +
                                            "\"start\": \"10/11/2014\"," +
                                            "\"end\": \"26/12/2014\"" +
                                        "}" +
                                    "]," +
                                    "\"percentageCrossed\":" +
                                    "[" +
                                       "{" +
                                           "\"percentage\": 30," +
                                           "\"timeoutPeriod\": \"00:15\"" +
                                        "}" +
                                   "]" +
                                "}" +
                            "}" +
                        "]" +
                    "}" +
                "}" +
            "}" +
        "}";
         return action;
        }

        private static String getJsonVibrationActionWithConditions()
        {
            String vibrationActionWithCondtionsJson =
                 "{" +
                    "\"security\": {" +
                        "\"apiKey\":" + "\"" + bdApplicationApiKey + "\"," +
                        "\"customerApiKey\":" + "\"" + bdCustomerApiKey + "\"" +
                    "}," +
                    "\"content\": {" +
                        "\"zone\": {" +
                            "\"zoneId\":" + "\"" + bdZoneId + "\"," +
                            "\"actions\": {" +
                            "\"vibrationActions\": [" +
                                "{" +
                                "\"name\": \"A vibration action\"" +
                                    "\"conditions\": {" +
                                        "\"percentageCrossed\":" +
                                            "[" +
                                                "{" +
                                                    "\"percentage\": 50," +
                                                    "\"timeoutPeriod\": \"00:05\"" +
                                                "}" +
                                            "]," +
                                        "\"dateRange\": [" +
                                            "{" +
                                                "\"start\": \"01/03/2014\"," +
                                                "\"end\": \"14/12/2014\"" +
                                            "}" +
                                        "]," +
                                        "\"timeActive\": [{" +
                                            "\"from\": {" +
                                                "\"time\": \"06:01\"," +
                                                "\"period\": \"am\" " +
                                            "}," +
                                            "\"to\": {" +
                                                "\"time\": \"11:00\"," +
                                                "\"period\": \"pm\" " +
                                            "}" +
                                        "}]" +
                                    "}" +
                                "}" +
                            "]" +
                          "}" +
                        "}" +
                    "}" +
                "}";
            return vibrationActionWithCondtionsJson;
        }
    }
}
