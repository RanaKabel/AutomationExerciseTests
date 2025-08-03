using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using Newtonsoft.Json;

using AutomationExerciseTests.Api;
using AutomationExerciseTests.Models;
using AutomationExerciseTests.Utils;

namespace AutomationExerciseTests.Tests
{
    [TestFixture]
    public class ApiAuthTests 
    {
        private ApiClient apiClient;
        private const string RegistrationDataPath = "TestData/registrationData.json";
        private const string LoginDataPath = "TestData/loginData.json";

        [OneTimeSetUp]
        public void Setup()
        {
            apiClient = new ApiClient();
        }

        public static TestData[] GetRegistrationData() =>
            JsonConvert.DeserializeObject<TestData[]>(File.ReadAllText(RegistrationDataPath));

        public static TestData[] GetLoginData() =>
            JsonConvert.DeserializeObject<TestData[]>(File.ReadAllText(LoginDataPath));

        [Test]
        [TestCaseSource(nameof(GetRegistrationData))]
        public async Task Test_APIRegistration(TestData data)
        {
            if (string.IsNullOrEmpty(data.ExpectedError))
            {
                data.Email = TestUtils.GenerateUniqueEmail(data.Email);
            }

            var response = await apiClient.CreateUserAccount(data);

            if (!string.IsNullOrEmpty(data.ExpectedError))
            {
                StringAssert.Contains(data.ExpectedError, response.Content, "Expected error message not found in response.");
            }
            else
            {
                StringAssert.Contains("User created!", response.Content, "User creation success message not found.");
            }
        }

        [Test]
        [TestCaseSource(nameof(GetLoginData))]
        public async Task Test_APILogin(TestData data)
        {
            if (string.IsNullOrEmpty(data.ExpectedError))
            {
                // Ensure user exists before login
                string uniqueEmail = TestUtils.GenerateUniqueEmail(data.Email);
                data.Email = uniqueEmail;

                var regResponse = await apiClient.CreateUserAccount(data);
                StringAssert.Contains("User created!", regResponse.Content, "Registration failed before login.");
            }

            var response = await apiClient.PerformLogin(data);

            if (!string.IsNullOrEmpty(data.ExpectedError))
            {
                StringAssert.Contains(data.ExpectedError, response.Content, "Login error message mismatch.");
            }
            else
            {
                StringAssert.Contains("User exists!", response.Content, "Login success message not found.");
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            apiClient.Dispose();
        }
    }
}
