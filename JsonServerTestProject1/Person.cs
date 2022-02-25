using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace JsonServerTestProject1
{
    [TestClass]
    public class Person
    {
        public int Id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string phnum { get; set; }
    }
    [TestClass]
    public class RestSharptestCase
    {
        RestClient client;
        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:3000");
        }
        [TestMethod]
        public void TestMethod1()
        {
            RestResponse response = getList();
            //assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Person> dataResponse = JsonConvert.DeserializeObject<List<Person>>(response.Content);
            Assert.AreEqual(4, dataResponse.Count);
        }
        private RestResponse getList()
        {
            //arrange
           RestRequest request = new RestRequest("/db", Method.Get);
            //act
            RestResponse response = client.ExecuteAsync(request).Result;
            return response;
        }
        /// <summary>
        /// Adding the people
        /// </summary>
        [TestMethod]
        public void AddPerson()
        {
            //arrange
            RestRequest request = new RestRequest("/db/create", Method.Post);
            JObject jobjectBody = new JObject();
            jobjectBody.Add("first_name", "rasi");
            jobjectBody.Add("last_name", "abc");
            jobjectBody.Add("email", "rasi@gmail.com");
            jobjectBody.Add("address", "hyd");
            jobjectBody.Add("phnum", "938444837");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", jobjectBody, ParameterType.RequestBody);
            
            //act
            RestResponse response = client.ExecuteAsync(request).Result;
            //assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Person person = JsonConvert.DeserializeObject<Person>(response.Content);
            Assert.AreEqual("priya", person.first_name);
            Assert.AreEqual("kkk", person.last_name);
            Assert.AreEqual("rasi@gmail.com", person.email);
            Assert.AreEqual("hyd", person.address);
            Assert.AreEqual("938444837", person.phnum);
            Console.WriteLine(response.Content);
        }
    }

}
    

