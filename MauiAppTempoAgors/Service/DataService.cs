﻿using MauiAppTempoAgors.Models;
using System.Diagnostics;
using NewtonSoft.json;
using Newtonsoft.json.Linq;
using Java.Interop;
using Xamarin.KotlinX.Coroutines;

namespace MauiAppTempoAgors.Service
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisaoDoTempo(string cidade)
        {
            // https://openweathermap.org/current#current_JSON
            // https://home.openwathermap.org/api_keys
            string appId = "6135072afe7f6cec1537d5cb08a5a1a2";

            string url = $"http://api.openweathermap.org/data/2.5/weather?q={cidade}&units=metric&appid={appId}";

            Tempo tempo = null;

            using (HttpClient client = new HttpClient()) 
            { 
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode) 
                { 
                    string json = response.Content.ReadAsStringAsync().Result;

                    var rascunho = JObject.Parse(json);

                    DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();
                }
            }

        }
    }
}
