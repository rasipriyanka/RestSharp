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
            // arrange
            RestRequest request = new RestRequest("/db/create", Method.Post);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(
            new Person
            {
                first_name = "rasi",
                last_name = "priyanka",
                email = "rasi@gmail.com",
                address = "9998765456",
                phnum = "Kmm"
            });
            // act
            RestResponse response = client.ExecuteAsync(request).Result;
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Person dataResponse = JsonConvert.DeserializeObject<Person>(response.Content);
            Assert.AreEqual("rasi", dataResponse.first_name);
            Assert.AreEqual("priyanka", dataResponse.last_name);
            Assert.AreEqual("rasi@gmail.com", dataResponse.email);
            Assert.AreEqual("9998765456", dataResponse.address);
            Assert.AreEqual("Kmm", dataResponse.phnum);
        }
    }

}
    

