﻿using Newtonsoft.Json;
using OloAutomationChallengeProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OloAutomationChallengeProject.Helpers
{
    public class RequestHelpers 
    {
        Factory factory = new Factory();
        public RequestHelpers()
        {

        }

        /// <summary>
        /// Setting up the URL used for Http Requests
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="endpoint"></param>
        /// <returns>string</returns>
        public string SetupUrl(string baseUrl, string endpoint)
        {
            var url = Path.Combine(baseUrl, endpoint);
            return url;
        }

        /// <summary>
        /// Makes an Http GET Request using the HttpClient
        /// </summary>
        /// <param name="url"></param>
        /// <returns>HttpResponseMessage</returns>
        public async Task<HttpResponseMessage> CreateGetRequestAsync(string url)
        {
            var client = factory.CreateNewClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Returns the Deserialized content object of an HttpRequest 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task<T> GetResponseContentAsync<T>(HttpResponseMessage response)
        {
            using var content = response.Content;
            var responseBody = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        /// <summary>
        /// Makes a Http POST Request using the HttpClient
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <returns>HttpResponseMessage</returns>
        public async Task<HttpResponseMessage> CreatePostRequestAsync(string url, object user)
        {
            var convertedJsonData = JsonConvert.SerializeObject(user);
            var data = new StringContent(convertedJsonData, Encoding.UTF8, "application/json");

            var client = factory.CreateNewClient();

            var response = await client.PostAsync(url, data);
            return response;
        }

        /// <summary>
        /// Makes a Http PUT Request using the HttpClient
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <returns>HttpResponseMessage</returns>
        public async Task<HttpResponseMessage> CreatePUTRequestAsync(string url, object user)
        {
            var convertedJsonData = JsonConvert.SerializeObject(user);
            var data = new StringContent(convertedJsonData, Encoding.UTF8, "application/json");

            var client = factory.CreateNewClient();

            var response = await client.PutAsync(url, data);
            return response;
        }

        /// <summary>
        /// Makes a Http DELETE Request using the HttpClient
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> CreateDeleteRequestASynch(string url)
        {
            var client = factory.CreateNewClient();

            var response = await client.DeleteAsync(url);
            return response;
        }
    }
}
