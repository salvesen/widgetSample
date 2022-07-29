using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace widgetSample
{
    public class postingToInterface
    {
        public static async Task<string> postToInterfaceNew(string linkExtension, string token = null, string JSON = null)
        {
            var body = new JObject();
            HttpClientHandler handler = new HttpClientHandler()
            {
            };

            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://www.example.com/endpoint");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var content = new StringContent(body.ToString(Newtonsoft.Json.Formatting.None), System.Text.Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage result = await client.PostAsync(client.BaseAddress + linkExtension, content).ConfigureAwait(false);
                    string resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return resultContent;
                }
            }
        }

        public static string getEuroToNokPrice(DateTime date)
        {
            try
            {
                //Check the current time:
                DateTime currentDateTime = new DateTime();
                currentDateTime = DateTime.Now;
                string year = date.Year.ToString();
                //Get the month
                double month = date.Month;
                //Get the day
                double day = date.Day;
                string dayBefore = "";
                string dayString = "";
                string monthString = "";
                if (day < 10)
                {
                    dayString = "0" + (day).ToString();
                    dayBefore = "0" + (day - 1).ToString();
                }
                else if (day == 10)
                {
                    dayString = (day).ToString();
                    dayBefore = "0" + (day - 1).ToString();
                }
                else
                {
                    dayBefore = (day - 1).ToString();
                    dayString = day.ToString();
                }
                if (month < 10)
                {
                    monthString = "0" + month.ToString();
                }
                else
                {
                    monthString = month.ToString();
                }

                //Norges bank quary Url https://app.norges-bank.no/query/index.html#/no/currency
                string url = string.Format("https://data.norges-bank.no/api/data/EXR/B.EUR.NOK.SP?format=sdmx-generic-2.1&startPeriod={0}-{1}-{2}&endPeriod={3}-{4}-{5}&locale=no", year, monthString, dayBefore, year, monthString, dayString);
                //Get to interface
                string responseFromNBank = getToInterface(url);
                return responseFromNBank;
            }
            catch (Exception e)
            {
                Console.WriteLine("Catched: " + e);
                loggingRelated.writeALineToLog("Catched getting from interface: " + e);
                return " ";
            }
        }

        private static string getToInterface(string url)
        {
            try
            {
                //Create the web socket
                HttpWebRequest webSocket = (HttpWebRequest)WebRequest.Create(url);

                //Read from socket
                var httpResponse = (HttpWebResponse)webSocket.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (WebException error)
            {
                HttpWebResponse response = (HttpWebResponse)error.Response;
                return "Error: failed to post to interface. " + error.Message;
            }
        }
    }
}
