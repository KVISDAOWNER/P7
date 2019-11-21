/*
 * Machine learning API
 *
 * API for machine learning on water probe measured data
 *
 * OpenAPI spec version: 1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using IO.Swagger.Attributes;

using Microsoft.AspNetCore.Authorization;
using IO.Swagger.Models;
using MongoDB.Driver;

namespace IO.Swagger.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class ProbeApiController : ControllerBase
    {
        public ProbeApiController(IMongoDBSettings dBSettings)
        {
            //MongoClient mongoClient = new MongoClient("mongodb://" + dBSettings.Host + ":" + dBSettings.Port);
            MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            mongoDB = mongoClient.GetDatabase(dBSettings.Database);
        }
        private readonly IMongoDatabase mongoDB;

        /// <summary>
        /// Gets all observations by a probe
        /// </summary>
        /// <param name="probename">Unique thingName</param>
        /// <response code="200">Successful response</response>
        /// <response code="404">Not created response</response>
        [HttpGet]
        [Route("/probe")]
        [ValidateModelState]
        [SwaggerOperation("GetProbeMeasurement")]
        [SwaggerResponse(statusCode: 200, type: typeof(Sample), description: "Successful response")]
        [SwaggerResponse(statusCode: 404, type: typeof(string), description: "Unsuccessful response")]
        public virtual IActionResult GetProbeMeasurement([FromQuery]string probename)
        {

            var probeCollection = mongoDB.GetCollection<Probe>("Probe");
            Probe probe = probeCollection.Find(x => x.Id == probename).FirstOrDefault();
            var sensorCollection = mongoDB.GetCollection<Sensor>("Sensor");
            var observedPropertyCollection = mongoDB.GetCollection<ObservedProperty>("observedProperty");
            List<string> sensorIDs = new List<string>();
            List<Tuple<string, List<Observation>>> results = new List<Tuple<string, List<Observation>>>();
            List<Sensor> sensors = new List<Sensor>();
            List<string> observedPropertyReferences = new List<string>();
            
            List<ObservedProperty> observedProperties = new List<ObservedProperty>();

            //Gets all sensors, need since sensor is the only entity that has an observedProperty
            sensors = sensorCollection.Find<Sensor>(x => true).ToList();

            foreach (Sensor sensor in sensors)
            {
                observedPropertyReferences.Add(sensor.ObservedPropertyRef);
            }

            foreach(string opRef in observedPropertyReferences)
            {
                observedProperties.Add(observedPropertyCollection.Find(x => x.Id == opRef).FirstOrDefault());   
            }

            
            // loop to get all attached sensors to a probe
            foreach (AttachedSensor sensor in probe.AttachedSensors)
            {
                sensorIDs.Add(sensor.RefToSensor);
                Sensor currentSensor = sensorCollection.Find(x => x.Id == sensor.RefToSensor).FirstOrDefault();
                ObservedProperty currentObservedProperty = observedPropertyCollection.Find(x => x.Id == currentSensor.ObservedPropertyRef).FirstOrDefault();
                List<Observation> currentDataList = new List<Observation>();
                var observationCollection = mongoDB.GetCollection<Observation>(currentSensor.Id + "_" + probename);

                //Not interested in returning the collections which are meant for containing predictions
                if (observationCollection.CollectionNamespace.CollectionName.StartsWith("MI_"))
                    continue;

                var observationList = observationCollection.Find(x => true).ToList();


                results.Add(Tuple.Create(currentSensor.Id + "_" + currentSensor.ObservedPropertyRef, observationList));
            }
            return new ObjectResult(JsonConvert.SerializeObject(results, Formatting.Indented));
        }


        /// <summary>
        /// Gets all observations by all probes
        /// </summary>
        /// <response code="201">Successful response</response>
        /// <response code="404">Not created response</response>
        [HttpGet]
        [Route("/probes")]
        [ValidateModelState]
        [SwaggerOperation("GetProbesMeasurements")]
        [SwaggerResponse(statusCode: 201, type: typeof(Sample), description: "Successful response")]
        [SwaggerResponse(statusCode: 404, type: typeof(string), description: "Not created response")]
        public virtual IActionResult GetProbesMeasurements()
        {
            List<Probe> probes = new List<Probe>();
            var probeCollection = mongoDB.GetCollection<Probe>("Probe");
            probes = probeCollection.Find(x => true).ToList();

            var sensorCollection = mongoDB.GetCollection<Sensor>("Sensor");
            var observedPropertyCollection = mongoDB.GetCollection<ObservedProperty>("observedProperty");

            List<string> sensorIDs = new List<string>();
            List<Tuple<string, List<Observation>>> results = new List<Tuple<string, List<Observation>>>();
            List<Sensor> sensors = new List<Sensor>();
            List<string> observedPropertyReferences = new List<string>();
            List<ObservedProperty> observedProperties = new List<ObservedProperty>();


            //Gets all sensors, need since sensor is the only entity that has an observedProperty
            sensors = sensorCollection.Find<Sensor>(x => true).ToList();

            List<Tuple<string, List<Tuple<string, List<Observation>>>>> resultsList = new List<Tuple<string, List<Tuple<string, List<Observation>>>>>();

            foreach (Probe probe in probes)
            {
                List<Tuple<string, List<Observation>>> intermediateResult = new List<Tuple<string, List<Observation>>>();
                var attachedSensorsList = probe.AttachedSensors.FindAll(x => true);
                foreach (AttachedSensor attachedSensor in attachedSensorsList)
                {
                    Sensor currentSensor = sensorCollection.Find(x => x.Id == attachedSensor.RefToSensor).FirstOrDefault();
                    var observationCollection = mongoDB.GetCollection<Observation>(currentSensor.Id + "_" + probe.Id);

                    //Not interested in returning the collections which are meant for containing predictions
                    if (observationCollection.CollectionNamespace.CollectionName.StartsWith("MI_"))
                        continue;
                    
                    var observationList = observationCollection.Find(x => true).ToList();
                    intermediateResult.Add(Tuple.Create(currentSensor.Id + "_" + currentSensor.ObservedPropertyRef, observationList));

                }

                resultsList.Add(Tuple.Create(probe.Id, intermediateResult));

            }

            return new ObjectResult(JsonConvert.SerializeObject(resultsList, Formatting.Indented));

        }

        /// <summary>
        /// Post mahcine learning predicted values
        /// </summary>
        /// <remarks>Creates new thing</remarks>
        /// <param name="body">Optional properties represented as json for the Thing</param>
        /// <response code="201">Successful response</response>
        /// <response code="404">Not created response</response>
        [HttpPost]
        [Route("/probes")]
        [ValidateModelState]
        [SwaggerOperation("NewThing")]
        [SwaggerResponse(statusCode: 201, type: typeof(Sample), description: "Successful response")]
        [SwaggerResponse(statusCode: 404, type: typeof(string), description: "Not created response")]
        public virtual IActionResult NewThing([FromBody] Prediction body)
        {
            try
            {
                string collectionForInserting = body.Sensor + body.Probe;
                var miValues = mongoDB.GetCollection<Observation>(collectionForInserting);

                List<Observation> obs = new List<Observation>();

                foreach (var ob in body.Values)
                {
                    obs.Add(new Observation(ob.PhenomenonTime, ob.ResultTime, ob.Result)); 
                }

                miValues.InsertMany(obs);

                return StatusCode(200, default(Sample));
            } 
            catch (Exception e)
            {
                return new ObjectResult(e.Message);
            }


            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(Sample));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(string));
            string exampleJson = null;
            exampleJson = "{\n  \"placeholder\" : \"placeholder\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Sample>(exampleJson)
                        : default(Sample);            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}
