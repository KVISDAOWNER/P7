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
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using SensorThingsAPI.Converters;
using MongoDB.Bson.Serialization.Attributes;
using SensorThingsAPI.Models.DTO;
using SensorThingsAPI.Models.DBModels;

namespace SensorThingsAPI.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Location : IEquatable<Location>
    {
        public Location(DTOLocation dTOLocation)
        {
            this.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            this.Description = dTOLocation.Description;
            this.EncodingType = dTOLocation.EncodingType;
            this._Location = dTOLocation._Location;
        }
        /// <summary>
        /// Gets or Sets the id
        /// </summary>
        [Required]
        [DataMember(Name = "_id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [Required]
        [BsonElement("description")]
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets EncodingType
        /// </summary>
        [Required]
        [BsonElement("encodingType")]
        [DataMember(Name="encodingType", EmitDefaultValue=false)]
        public string EncodingType { get; set; }

        /// <summary>
        /// Gets or Sets _Location
        /// </summary>
        [Required]
        [BsonElement("location")]
        [DataMember(Name="location", EmitDefaultValue=false)]
        public LatLong _Location { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Location {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  EncodingType: ").Append(EncodingType).Append("\n");
            sb.Append("  _Location: ").Append(_Location).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Location)obj);
        }

        /// <summary>
        /// Returns true if Location instances are equal
        /// </summary>
        /// <param name="other">Instance of Location to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Location other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Description == other.Description ||
                    Description != null &&
                    Description.Equals(other.Description)
                ) && 
                (
                    EncodingType == other.EncodingType ||
                    EncodingType != null &&
                    EncodingType.Equals(other.EncodingType)
                ) && 
                (
                    _Location == other._Location ||
                    _Location != null &&
                    _Location.Equals(other._Location)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                    if (EncodingType != null)
                    hashCode = hashCode * 59 + EncodingType.GetHashCode();
                    if (_Location != null)
                    hashCode = hashCode * 59 + _Location.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Location left, Location right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Location left, Location right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
