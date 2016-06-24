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

/**
 * @author Bluedot Innovation
 * Copyright (c) 2016 Bluedot Innovation. All rights reserved.
 * Get Application client demonstrates listing an application for a given apiKey from Bluedot backend.
 */
namespace BluedotPublicApiClient.applicationclient
{
    public class GetApplication
    {
        private static String apiKey         = "b817bb5d-4b58-4f41-be5f-528fd4c7c95c";
        private static String customerApiKey = "ca4c8d11-6942-11e4-ba4b-a0481cdc3311";
        private static String bdRestUrl      = "https://api.bluedotinnovation.com/1/applications?customerApiKey=" + customerApiKey + "&apiKey=" + apiKey;

        public void getApplications()
        {
            HttpClient httpRestClient = new HttpClient();

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
