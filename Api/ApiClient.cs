// File: ApiClient.cs
// Purpose: Handles API requests for user authentication and registration using AutomationExercise.com API

using System;
using System.Net;
using System.Threading.Tasks;
using AutomationExerciseTests.Models;
using RestSharp;

namespace AutomationExerciseTests.Api
{
    public class ApiClient : IDisposable
    {
        private readonly RestClient _client;
        private const string BaseUrl = "https://automationexercise.com/api";

        public ApiClient()
        {
            var options = new RestClientOptions(BaseUrl)
            {
                ThrowOnAnyError = false,
                MaxTimeout = 10000
            };
            _client = new RestClient(options);
        }

        public async Task<RestResponse> CreateUserAccount(TestData data)
        {
            var request = new RestRequest("createAccount", Method.Post);

            request.AddParameter("name", data.Name);
            request.AddParameter("email", data.Email);
            request.AddParameter("password", data.Password);
            request.AddParameter("title", data.Title ?? "Mr");
            request.AddParameter("birth_date", data.BirthDate ?? "1");
            request.AddParameter("birth_month", data.BirthMonth ?? "January");
            request.AddParameter("birth_year", data.BirthYear ?? "1990");
            request.AddParameter("firstname", data.FirstName ?? data.Name);
            request.AddParameter("lastname", data.LastName ?? "Test");
            request.AddParameter("company", data.Company ?? "TestCompany");
            request.AddParameter("address1", data.Address1 ?? "123 Test St");
            request.AddParameter("address2", data.Address2 ?? "Suite 100");
            request.AddParameter("country", data.Country ?? "United States");
            request.AddParameter("zipcode", data.ZipCode ?? "12345");
            request.AddParameter("state", data.State ?? "TestState");
            request.AddParameter("city", data.City ?? "TestCity");
            request.AddParameter("mobile_number", data.MobileNumber ?? "+1234567890");

            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> PerformLogin(TestData data)
        {
            var request = new RestRequest("verifyLogin", Method.Post);
            request.AddParameter("email", data.Email);
            request.AddParameter("password", data.Password);
            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> VerifyLoginWithoutEmail(string password)
        {
            var request = new RestRequest("verifyLogin", Method.Post);
            request.AddParameter("password", password);
            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> VerifyLoginWithInvalidDetails(string email, string password)
        {
            var request = new RestRequest("verifyLogin", Method.Post);
            request.AddParameter("email", email);
            request.AddParameter("password", password);
            return await _client.ExecuteAsync(request);
        }

        public bool ValidateResponse(RestResponse response, TestData data)
        {
            // Match status code if provided
            if ((int)response.StatusCode != data.ExpectedStatusCode)
                return false;

            // Match expected message if provided
            if (!string.IsNullOrEmpty(data.ExpectedError))
                return response.Content?.Contains(data.ExpectedError) ?? false;

            // If no error expected, treat status code match as success
            return true;
        }

        public void Dispose()
        {
            // No unmanaged resources to release, placeholder for IDisposable
        }
    }
}
