using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

using AutomationExerciseTests.PageObjects;
using AutomationExerciseTests.Utils;
using AutomationExerciseTests.Models;

namespace AutomationExerciseTests.Tests
{
    [TestFixture]
    public class RegistrationAndLoginTests
    {
        private IWebDriver driver;
        private const string BaseUrl = "https://www.automationexercise.com";
        private const string TestDataPath = "TestData/registrationData.json";

        [SetUp]
        public void Setup()
        {
            var options = new FirefoxOptions();
            // options.AddArgument("--headless"); // Uncomment for headless testing
            driver = new FirefoxDriver(options);
            driver.Manage().Window.Size = new Size(1920, 1080);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Navigate().GoToUrl(BaseUrl);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        public class RegistrationTestData
        {
            public string TestCase { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Title { get; set; }
            public string BirthDate { get; set; }
            public string BirthMonth { get; set; }
            public string BirthYear { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Company { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Country { get; set; }
            public string ZipCode { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string MobileNumber { get; set; }
            public int ExpectedStatusCode { get; set; }
            public string ExpectedError { get; set; }
        }

        public static IEnumerable<RegistrationTestData> TestData()
        {
            var json = File.ReadAllText(TestDataPath);
            return JsonConvert.DeserializeObject<List<RegistrationTestData>>(json);
        }

        [Test]
        [TestCaseSource(nameof(TestData))]
        public void RegistrationThenLoginTest(RegistrationTestData testData)
        {
            var homePage = new HomePage(driver);
            var signupLoginPage = new SignupLoginPage(driver);

            driver.Navigate().GoToUrl(BaseUrl);
            homePage.ClickSignupLogin();

            string emailToUse = testData.Email;

            if (testData.TestCase == "ValidRegistration")
            {
                emailToUse = TestUtils.GenerateUniqueEmail(testData.Email);
            }

            signupLoginPage.RegisterUser(testData.Name, emailToUse);

            if (testData.TestCase == "DuplicateRegistration")
            {
                string error = signupLoginPage.GetErrorMessage()?.ToLower() ?? "";

                bool isValidError =
                    error.Contains("email") &&
                    (error.Contains("exist") || error.Contains("exists"));

                Assert.IsTrue(isValidError, $"Unexpected error message: '{error}'");
                return;
            }

            var createAccountPage = new CreateAccountPage(driver);

            createAccountPage.SelectTitle(testData.Title);
            createAccountPage.EnterPassword(testData.Password);
            createAccountPage.SelectDateOfBirth(
                testData.BirthDate,
                testData.BirthMonth,
                testData.BirthYear
            );
            createAccountPage.SetNewsletterSubscription(true);
            createAccountPage.SetOffersSubscription(true);

            createAccountPage.EnterAddressInformation(
                testData.FirstName,
                testData.LastName,
                testData.Company,
                testData.Address1,
                testData.Address2,
                testData.Country,
                testData.State,
                testData.City,
                testData.ZipCode,
                testData.MobileNumber
            );

            createAccountPage.ClickCreateAccount();

            var bodyText = driver.FindElement(By.TagName("body")).Text;
            Assert.IsTrue(
                bodyText.IndexOf("account created", StringComparison.OrdinalIgnoreCase) >= 0,
                "Expected text 'account created' was not found."
            );
        }
    }
}
