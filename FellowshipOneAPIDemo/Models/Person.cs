using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;


namespace FellowshipOneAPIDemo.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Salutation { get; set; }
        public string Prefix { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int HouseholdId { get; set; }
    }

    public class Address
    {
        
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlElement("household")]
        public int HouseholdId { get; set; }
        public string AddressType { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string StProvince { get; set; }
        public string CarrierRoute { get; set; }
        public string DeliveryPoint { get; set; }
    }

    public class Communication
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public int PersonId { get; set; }
        public string CommunicationType { get; set; }
        public string CommunicationGeneralType { get; set; }
        public string CommunicationValue { get; set; }
        public string SearchCommunicationValue { get; set; }
        public string Preferred { get; set; }
        public string CommunicationComment { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
        
    
    }
    

}