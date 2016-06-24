﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Security.Cryptography.X509Certificates;

/**
 * @author Bluedot Innovation
 * Copyright (c) 2016 Bluedot Innovation. All rights reserved.
 * Get Zone client demonstrates the listing of zone details for a given customer
 */

namespace BluedotPublicApiClient.zoneclient
{
    public class GetZone
    {
        private static String bdCustomerApiKey      = "76e1ae30-c616-11e5-a7c0-b8ca3a6b879d"; //This apiKey is generated when you create an application
        private static String bdApplicationApiKey   = "dee11930-ebff-11e5-8e27-bc305bf60831"; //This key is generated by Bluedot Access UI when you register
        private static String bdZoneId              = "67f99448-a646-43c9-a6ae-0d823d65edbd"; //This is the id of the zone being retrieved
        private static String bdRestUrl             = "https://api.bluedotinnovation.com/1/zones?customerApiKey=" + bdCustomerApiKey + "&apiKey=" + bdApplicationApiKey + 
                                                            "&zoneId=" + bdZoneId; 
        
        public void getZoneByZoneId()
        {
            WebRequestHandler handler = new WebRequestHandler();
            X509Certificate2 certificate = new X509Certificate2();
            handler.ClientCertificates.Add(certificate);
            HttpClient httpRestClient = new HttpClient(handler);

            //specify to use TLS 1.2 as default connection
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            HttpResponseMessage serverResponse = httpRestClient.GetAsync(new Uri(bdRestUrl)).Result;
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
    }
}
