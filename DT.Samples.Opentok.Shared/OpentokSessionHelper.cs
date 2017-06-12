﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DT.Samples.Opentok.Shared
{
    public static class OpentokSessionHelper
    {
        public const string SessionRequestURI = "https://dt-video.azurewebsites.net/opentok/session/{0}";
        public const string TokenRequestURI = "https://dt-video.azurewebsites.net/opentok/token/{0}";

        public static async Task<string> Request(string urlFormat, string value)
        {
            var url = string.Format(urlFormat, value);
            try
            {
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<OpentokRequest>(result).Result;
            }
            catch (Exception ex)
            {
            }
            return string.Empty;
        }
    }
}
