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
* Add fence client demonstrates adding fences of various shapes to an existing zone using net http web api library
* Circular fence
* Bounding Box
* Polygonal
* Geoline
*/

namespace BluedotPublicApiClient.fenceclient
{
    public class BDAddFenceClient
    {
        private static String bdApplicationApiKey = "a46fc46a-63ac-4c0c-8a9c-3c9aafd88e46"; //This apiKey is generated when you create an application
        private static String bdCustomerApiKey = "944ab370-7a0b-11e4-828c-a0481cdc3311"; //This key is generated by Bluedot Point Access UI when your account is created
        private static String bdZoneId = "cc6f9dd1-3d69-454d-abdd-58176cbf67dc"; //This is the id of the zone being updated. This can be fetched by calling zones/getAll API
        private static String bdRestUrl = "https://api.bluedotinnovation.com/1/fences";

        public void addFence()
        {
            WebRequestHandler handler = new WebRequestHandler();
            X509Certificate2 certificate = new X509Certificate2();
            handler.ClientCertificates.Add(certificate);
            HttpClient httpRestClient = new HttpClient(handler);

            //specify to use TLS 1.2 as default connection
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            httpRestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent jsonFenceContent = new StringContent(getJsonPolygonalFence());
            jsonFenceContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage serverResponse   = httpRestClient.PostAsync(new Uri(bdRestUrl), jsonFenceContent).Result;
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

        /*Circular fence requires a centerpoint and radius*/
        private static String getJsonCircularFence()
        {
            String circularFenceJson =
            "{" +
                "\"security\": {" +
                 "\"apiKey\":" + "\"" + bdApplicationApiKey + "\"," +
                 "\"customerApiKey\":" + "\"" + bdCustomerApiKey + "\" " +
             "}," +
             "\"content\": {" +
                 "\"zone\": {" +
                     "\"zoneId\":" + "\"" + bdZoneId + "\"," +
                     "\"fences\": {" +
                         "\"circles\": [" +
                             "{" +
                                 "\"order\": 1," +
                                 "\"name\": \"Fence-0\"," +
                                 "\"color\": \"#b3a0d\"," +
                                 "\"radius\": 12.855266312171226," +
                                 "\"center\": {" +
                                     "\"latitude\": \"-37.81868567196429\"," +
                                     "\"longitude\": \"144.98012825841897\" " +
                                 "}" +
                             "}" +
                         "]" +
                     "}" +
                 "}" +
             "}" +
           "}";
           return circularFenceJson;
        }

        /*Bounding box requires north east and south west points*/
        private static String getJsonBoundingBox()
        {
            String boundingBoxFenceJson =
                    "{" +
                       "\"security\": {" +
                            "\"apiKey\":" + "\"" + bdApplicationApiKey + "\"," +
                            "\"customerApiKey\":" + "\"" + bdCustomerApiKey + "\" " +
                    "}," +
                    "\"content\": {" +
                        "\"zone\": {" +
                            "\"zoneId\":" + "\"" + bdZoneId + "\"," +
                         "\"fences\": {" +
                            "\"rectangles\": [" +
                                "{" +
                                    "\"order\": 2," +
                                    "\"name\": \"Bounding Box-1\"," +
                                    "\"color\": \"#3dcb69\"," +
                                    "\"northEast\": {" +
                                        "\"latitude\": -37.81864541435753," +
                                        "\"longitude\": 144.98078003525734" +
                                    "}," +
                                    "\"southWest\": {" +
                                        "\"latitude\": -37.81885941545376," +
                                        "\"longitude\": 144.98049572110176" +
                                    "}" +
                                "}" +
                            "]" +
                        "}" +
                    "}" +
                 "}" +
               "}";
            return boundingBoxFenceJson;
        }

        /*Polygonal fence requires a series points in lat/long*/
        private static String getJsonPolygonalFence()
        {
            String polygonalFenceJson =
                           "{" +
                               "\"security\": {" +
                                   "\"apiKey\":" + "\"" + bdApplicationApiKey + "\"," +
                                   "\"customerApiKey\":" + "\"" + bdCustomerApiKey + "\" " +
                            "}," +
                            "\"content\": {" +
                                "\"zone\": {" +
                                    "\"zoneId\":" + "\"" + bdZoneId + "\"," +
                                    "\"fences\": {" +
                                    "\"polygons\": [" +
                                        "{" +
                                            "\"order\": 3," +
                                            "\"name\": \"Polygon-1\"," +
                                            "\"color\": \"#a0d7be\"," +
                                            "\"vertices\": [" +
                                                 "{" +
                                                     "\"latitude\": -37.81527640444762," +
                                                     "\"longitude\": 144.975049495697" +
                                                 "}," +
                                                 "{" +
                                                    "\"latitude\": -37.80735968553275," +
                                                    "\"longitude\": 144.9712514877319" +
                                                 "}," +
                                                 "{" +
                                                    "\"latitude\": -37.80581692842606," +
                                                    "\"longitude\": 144.9556946754456" +
                                                 "}," +
                                                 "{" +
                                                    "\"latitude\": -37.80500315345196," +
                                                    "\"longitude\": 144.9490320682526" +
                                                 "}," +
                                                  "{" +
                                                    "\"latitude\": -37.80698671424123," +
                                                    "\"longitude\": 144.9468326568604" +
                                                 "}," +
                                                    "{" +
                                                    "\"latitude\": -37.80927537202523," +
                                                    "\"longitude\": 144.9441075325012" +
                                                 "}," +
                                                 
                                                   "{" +
                                                    "\"latitude\": -37.812004715604," +
                                                    "\"longitude\": 144.9460601806641" +
                                                 "}," +
                                                  "{" +
                                                    "\"latitude\": -37.81137748408285," +
                                                    "\"longitude\": 144.9477124214172" +
                                                 "}," 
                                                 +
                                                  "{" +
                                                    "\"latitude\": -37.81346258449963," +
                                                    "\"longitude\": 144.949836730957" +
                                                 "}," 
                                                    +
                                                  "{" +
                                                    "\"latitude\": -37.82136169906169," +
                                                    "\"longitude\": 144.9547076225281" +
                                                 "}" +
                                            "]" +
                                        "}" +
                                    "]" +
                                "}" +
                            "}" +
                         "}" +
                    "}";
            return polygonalFenceJson;
        }

        /*Geoline fence requires a series points in lat/long*/
        private static String getJsonGeolineFence()
        {
            String geolineFenceJson =
                           "{" +
                               "\"security\": {" +
                                   "\"apiKey\":" + "\"" + bdApplicationApiKey + "\"," +
                                   "\"customerApiKey\":" + "\"" + bdCustomerApiKey + "\" " +
                            "}," +
                            "\"content\": {" +
                                "\"zone\": {" +
                                    "\"zoneId\":" + "\"" + bdZoneId + "\"," +
                                    "\"fences\": {" +
                                    "\"polylines\": [" +
                                        "{" +
                                            "\"order\": 4," +
                                            "\"name\": \"Geoline-1\"," +
                                            "\"color\": \"#a0d7be\"," +
                                            "\"vertices\": [" +
                                                 "{" +
                                                     "\"latitude\": -37.81527640444762," +
                                                     "\"longitude\": 144.975049495697" +
                                                 "}," +
                                                 "{" +
                                                    "\"latitude\": -37.80735968553275," +
                                                    "\"longitude\": 144.9712514877319" +
                                                 "}," +
                                                 "{" +
                                                    "\"latitude\": -37.80581692842606," +
                                                    "\"longitude\": 144.9556946754456" +
                                                 "}," +
                                                  "{" +
                                                    "\"latitude\": -37.82136169906169," +
                                                    "\"longitude\": 144.9547076225281" +
                                                 "}" +
                                            "]" +
                                        "}" +
                                    "]" +
                                "}" +
                            "}" +
                         "}" +
                    "}";
            return geolineFenceJson;
        }
    }
}
