/*
 * WaterProbe API
 *
 * API for waterprobing
 *
 * The version of the OpenAPI document: 1
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using SensorThingsAPI.Attributes;
using Microsoft.AspNetCore.Authorization;
using SensorThingsAPI.Models;

namespace SensorThingsAPI.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class LocationApiController : ControllerBase
    { 
        /// <summary>
        /// deletes a sensor
        /// </summary>
        /// <param name="locationName">Unique locationName</param>
        /// <response code="200">Successful response</response>
        /// <response code="404">Not created response</response>
        [HttpDelete]
        [Route("/Location")]
        [ValidateModelState]
        [SwaggerOperation("DeleteLocation")]
        [SwaggerResponse(statusCode: 200, type: typeof(Sample), description: "Successful response")]
        [SwaggerResponse(statusCode: 404, type: typeof(string), description: "Not created response")]
        public virtual IActionResult DeleteLocation([FromQuery]string locationName)
        { 

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Sample));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(string));
            string exampleJson = null;
            exampleJson = "{\r\n  \"placeholder\" : \"placeholder\"\r\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<Sample>(exampleJson)
            : default(Sample);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// gets a location
        /// </summary>
        /// <param name="locationName">Unique locationName</param>
        /// <response code="200">Successful response</response>
        /// <response code="404">Not created response</response>
        [HttpGet]
        [Route("/Location")]
        [ValidateModelState]
        [SwaggerOperation("GetLocation")]
        [SwaggerResponse(statusCode: 200, type: typeof(Sample), description: "Successful response")]
        [SwaggerResponse(statusCode: 404, type: typeof(string), description: "Not created response")]
        public virtual IActionResult GetLocation([FromQuery]string locationName)
        { 

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Sample));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(string));
            string exampleJson = null;
            exampleJson = "{\r\n  \"placeholder\" : \"placeholder\"\r\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<Sample>(exampleJson)
            : default(Sample);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// posting a new location
        /// </summary>
        /// <param name="location"></param>
        /// <response code="201">Successful response</response>
        /// <response code="404">Not created response</response>
        [HttpPost]
        [Route("/Location")]
        [ValidateModelState]
        [SwaggerOperation("Postlocation")]
        [SwaggerResponse(statusCode: 201, type: typeof(Sample), description: "Successful response")]
        [SwaggerResponse(statusCode: 404, type: typeof(string), description: "Not created response")]
        public virtual IActionResult Postlocation([FromBody]Location location)
        { 

            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(Sample));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(string));
            string exampleJson = null;
            exampleJson = "{\r\n  \"placeholder\" : \"placeholder\"\r\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<Sample>(exampleJson)
            : default(Sample);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// updates a location
        /// </summary>
        /// <param name="location"></param>
        /// <response code="200">Successful response</response>
        /// <response code="404">Not created response</response>
        [HttpPut]
        [Route("/Location")]
        [ValidateModelState]
        [SwaggerOperation("PutLocation")]
        [SwaggerResponse(statusCode: 200, type: typeof(Sample), description: "Successful response")]
        [SwaggerResponse(statusCode: 404, type: typeof(string), description: "Not created response")]
        public virtual IActionResult PutLocation([FromBody]Location location)
        { 

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Sample));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(string));
            string exampleJson = null;
            exampleJson = "{\r\n  \"placeholder\" : \"placeholder\"\r\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<Sample>(exampleJson)
            : default(Sample);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}
