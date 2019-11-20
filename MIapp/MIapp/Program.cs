using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MIapp
{

    class Observation
    {
        public Observation(string PhenomenonTime, string ResultTime, double Result)
        {
            this.PhenomenonTime = PhenomenonTime;
            this.ResultTime = ResultTime;
            this.Result = Result;
        }
        public string PhenomenonTime { get; set; }
        public string ResultTime { get; set; }
        public double Result { get; set; }
    }

    class Prediction
    {
        public Prediction(string Probe, string Sensor, List<Observation> Values)
        {
            this.Probe = Probe;
            this.Sensor = Sensor;
            this.Values = Values;
        }
        public string Probe { get; set; }

        public string Sensor { get; set; }

        public List<Observation> Values { get; set; }

    }

    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Type the id for the probe you want to make predictions for");
            string id = Console.ReadLine();


            Communicator c = new Communicator(id);
            c.run();
        } 
    }

    class Communicator
    {
        public Communicator(string probeId)
        {
            iD = probeId;
        }

        private const string URL = "http://localhost:50352/";
        private const string GET = "probe?=";
        private const string POST = "probes";
        private string iD;
        private const string PREDICTIONCATEGORY = "MI_Phosphorus_";

        //The Run method both gets the measurements for a specific probe and it does also post values which have been altered
        public void run()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            List<Observation> results = new List<Observation>();

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(GET + iD).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var resp = response.Content.ReadAsStringAsync().Result;
                var dataObjects = JsonConvert.DeserializeObject<List<Tuple<string, List<Observation>>>>(JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result));
                foreach (var lst in dataObjects)
                {
                    foreach (var tup in lst.Item2)
                    {
                        if ((lst.Item1.StartsWith("MI_")))
                            continue;

                        results.Add(new Observation(tup.PhenomenonTime, tup.ResultTime, tup.Result + 5));
                    }
                }

                Prediction p = new Prediction(iD, PREDICTIONCATEGORY, results);

                try
                {
                    var responser = client.PostAsJsonAsync(POST, p);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                client.Dispose();
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            client.Dispose();
        }
    }
}

