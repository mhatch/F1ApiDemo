using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using FellowshipOneAPIDemo.Models;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
using FellowshipOneAPIDemo.Helpers;


namespace FellowshipOneAPIDemo.Helpers
{

    /// <summary>
    /// A class containing methods to simplify calls to the api
    /// </summary>
    public class ApiHelper
    {
        // api base url
        private string apiURL = ConfigurationManager.AppSettings["FellowshipApi"];

        /// <summary>
        /// This method calls the api and stuffs a Person object with the results
        /// </summary>
        /// <param name="id">Person ID</param>
        /// <returns>Person objects</returns>
        public Person GetPerson(int id)
        {
            // create web client and call the api
            var endPoint = String.Format("{0}/People/{1}?mode=demo", apiURL, id);
            var client = new RestClient(endPoint);
            var result = client.MakeRequest();

            // load results into xml doc
            XmlDocument result_doc = new XmlDocument();
            result_doc.LoadXml(result);

            // return person list
            return this.PopulatePerson(result_doc.SelectSingleNode("//person"));
          
            
        }

        /// <summary>
        /// This method executes a search request to the api
        /// </summary>
        /// <param name="search_params">A person object with first and last name populated</param>
        /// <returns> A list of Person objects</returns>
        public List<Person> Search(FellowshipOneAPIDemo.Models.Person search_params)
        {
            // construct and encode search string
            var criteria = String.Format("{0},{1}", search_params.LastName, search_params.FirstName);
            criteria = HttpUtility.UrlEncode(criteria);

            // create web client and call the api
            var endPoint = String.Format("{0}/People/Search?searchFor={1}&page=1&recordsperpage=10&mode=demo", apiURL, criteria);
            var client = new RestClient(endPoint);
            var result = client.MakeRequest();

            // load results into xml doc
            XmlDocument result_doc = new XmlDocument();
            result_doc.LoadXml(result);

            // return person list
            return this.PopulatePeople(result_doc);
        }

        /// <summary>
        /// This method gets all of the people in a household
        /// </summary>
        /// <param name="person_id">The id of the person</param>
        /// <returns>List of Person objects</returns>
        public List<Person> GetHousehold(int person_id)
        {
            // create web client and call the api
            var endPoint = String.Format("{0}/households/{1}/people?mode=demo&format=xml", apiURL, person_id.ToString());
            var client = new RestClient(endPoint);
            var result = client.MakeRequest();

            // load results into xml doc
            XmlDocument result_doc = new XmlDocument();
            result_doc.LoadXml(result);

            // return person list
            return this.PopulatePeople(result_doc);
        
        }

        /// <summary>
        /// This method gets all addresses for a person
        /// </summary>
        /// <param name="person_id">The id of the person</param>
        /// <returns>List of Address objects</returns>
        public List<Address> GetAddresses(int person_id)
        { 

            // create web client and call the api
            var endPoint = String.Format("{0}/People/1632903/Addresses?mode=demo&format=xml", apiURL, person_id.ToString());
            var client = new RestClient(endPoint);
            var result = client.MakeRequest();

            // load results into xml doc
            XmlDocument result_doc = new XmlDocument();
            result_doc.LoadXml(result);

            return this.PopulateAddresses(result_doc);
        }

        public bool CreateAddress(Address model) 
        {

            //https://demo.fellowshiponeapi.com/v1/People/1635398/Addresses

            XmlSerializer serializer = new XmlSerializer(model.GetType());
            var create_header = new XmlDocument();
            var nav = create_header.CreateNavigator();
            using (var writer = nav.AppendChild())
            {
                var ser = new XmlSerializer(model.GetType());
                ser.Serialize(writer, model);
            }


            return true;
        }

        /// <summary>
        /// This method gets all communications for a person
        /// </summary>
        /// <param name="person_id">The id of the person</param>
        /// <returns>List of Communication objects</returns>
        public List<Communication> GetCommunications(int person_id)
        {
            // create web client and call the api
            var endPoint = String.Format("{0}/People/{1}/Communications?format=xml&mode=demo", apiURL, person_id.ToString());
            var client = new RestClient(endPoint);
            var result = client.MakeRequest();

            // load results into xml doc
            XmlDocument result_doc = new XmlDocument();
            result_doc.LoadXml(result);

            return this.PopulateCommunications(result_doc);

        }


        /// <summary>
        /// This method deletes an Address
        /// </summary>
        /// <param name="address_id">Address ID</param>
        /// <returns>http response</returns>
        public string DeleteAddress(int address_id)
        {

            var endPoint = String.Format("{0}/Addresses/{1}/delete&mode=demo", apiURL, address_id.ToString());
            var client = new RestClient(endPoint);
            var result = client.MakeRequest();

            return result;

        
        }




        /// <summary>
        /// This method populates Person objects from xml returned by api
        /// </summary>
        /// <param name="doc">XmlDocument of api results</param>
        /// <returns>List of Person objects</returns>
        private List<Person> PopulatePeople(XmlDocument doc) {
            // create return list
            var people = new List<FellowshipOneAPIDemo.Models.Person>();

            // find all person nodes and create person objects
            XmlNodeList nlPeople = doc.SelectNodes("//person");
            foreach (XmlNode ndPerson in nlPeople)
            {
                people.Add(this.PopulatePerson(ndPerson));
            }

            return people;
        
        
        }

        /// <summary>
        /// This method populates Person objects from xml returned by api
        /// </summary>
        /// <param name="ndPerson">person api node</param>
        /// <returns>A Person object</returns>
        private Person PopulatePerson(XmlNode ndPerson)
        {
            var fname = ndPerson.SelectSingleNode("./firstName").InnerText;
            var lname = ndPerson.SelectSingleNode("./lastName").InnerText;
            var household_id = Convert.ToInt32(ndPerson.Attributes["householdID"].Value);
            var id = Convert.ToInt32(ndPerson.Attributes["id"].Value);
            return new FellowshipOneAPIDemo.Models.Person { FirstName = fname, LastName = lname, HouseholdId = household_id, Id = id };
        }


        /// <summary>
        /// This method populates Address objects from xml returned by api
        /// </summary>
        /// <param name="doc">XmlDocument of api results</param>
        /// <returns>List of Address objects</returns>
        private List<Address> PopulateAddresses(XmlDocument doc)
        {
            // create return list
            var addresses = new List<FellowshipOneAPIDemo.Models.Address>();

            // find all person nodes and create person objects
            XmlNodeList nlAddresses = doc.SelectNodes("//address");
            foreach (XmlNode ndAddress in nlAddresses)
            {
                var address = new FellowshipOneAPIDemo.Models.Address();

                address.Id = Convert.ToInt32(ndAddress.Attributes["id"].Value);
                address.HouseholdId = Convert.ToInt32(ndAddress.SelectSingleNode("./household/@id").Value);
                address.AddressType = ndAddress.SelectSingleNode("./addressType/name").InnerText;
                address.Address1 = ndAddress.SelectSingleNode("./address1").InnerText;
                address.Address2 = ndAddress.SelectSingleNode("./address2").InnerText;
                address.Address3 = ndAddress.SelectSingleNode("./address3").InnerText;
                address.City = ndAddress.SelectSingleNode("./city").InnerText;
                address.StProvince = ndAddress.SelectSingleNode("./stProvince").InnerText;
                address.PostalCode = ndAddress.SelectSingleNode("./postalCode").InnerText;
                address.County = ndAddress.SelectSingleNode("./county").InnerText;
                address.Country = ndAddress.SelectSingleNode("./country").InnerText;
                address.PostalCode = ndAddress.SelectSingleNode("./postalCode").InnerText;
                address.CarrierRoute = ndAddress.SelectSingleNode("./carrierRoute").InnerText;
                address.DeliveryPoint = ndAddress.SelectSingleNode("./deliveryPoint").InnerText;
                   
                addresses.Add(address);
            }

            return addresses;
        }

        /// <summary>
        /// This method populates Communication objects from xml returned by api
        /// </summary>
        /// <param name="doc">XmlDocument of api results</param>
        /// <returns>List of communication objects</returns>
        private List<Communication> PopulateCommunications(XmlDocument doc)
        {
            var communications = new List<FellowshipOneAPIDemo.Models.Communication>();

            // find all person nodes and create person objects
            XmlNodeList nlCommunications = doc.SelectNodes("//communication");
            foreach (XmlNode ndCommunication in nlCommunications)
            {
                var communication = new FellowshipOneAPIDemo.Models.Communication();
                communication.Id = Convert.ToInt32(ndCommunication.Attributes["id"].Value);
                communication.HouseholdId = Convert.ToInt32(ndCommunication.SelectSingleNode("./household/@id").Value);

                // some communications did not have person ids which seems strange but I thought it might just be the test data
                if (String.IsNullOrEmpty(ndCommunication.SelectSingleNode("./person/@id").Value))
                    communication.PersonId = -1;
                else
                    communication.PersonId = Convert.ToInt32(ndCommunication.SelectSingleNode("./person/@id").Value);

                communication.CommunicationType = ndCommunication.SelectSingleNode("./communicationType/name").InnerText;
                communication.CommunicationGeneralType = ndCommunication.SelectSingleNode("./communicationGeneralType").InnerText;
                communication.CommunicationValue = ndCommunication.SelectSingleNode("./communicationValue").InnerText;
                communication.SearchCommunicationValue = ndCommunication.SelectSingleNode("./searchCommunicationValue").InnerText;
                communication.Preferred = ndCommunication.SelectSingleNode("./searchCommunicationValue").InnerText;
                communication.CreatedDate = ndCommunication.SelectSingleNode("./createdDate").InnerText;
                communication.LastUpdatedDate = ndCommunication.SelectSingleNode("./lastUpdatedDate").InnerText;

                communications.Add(communication);
            }

            return communications;
        
        }
    
    
    }


}