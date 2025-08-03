using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;



namespace AutomationExerciseTests.PageObjects
{
    public class CreateAccountPage
    {
        private readonly IWebDriver driver;

        // Title radio buttons
        private readonly By titleMrRadio = By.Id("id_gender1");
        private readonly By titleMrsRadio = By.Id("id_gender2");

        // Name and Email (Email is disabled, so no input)
        private readonly By nameInput = By.Id("name");
        private readonly By emailInput = By.Id("email"); // Disabled, but can get value if needed

        // Password
        private readonly By passwordInput = By.Id("password");

        // Date of birth dropdowns
        private readonly By daysDropdown = By.Id("days");
        private readonly By monthsDropdown = By.Id("months");
        private readonly By yearsDropdown = By.Id("years");

        // Newsletter & Offers checkboxes
        private readonly By newsletterCheckbox = By.Id("newsletter");
        private readonly By offersCheckbox = By.Id("optin");

        // Address Information fields
        private readonly By firstNameInput = By.Id("first_name");
        private readonly By lastNameInput = By.Id("last_name");
        private readonly By companyInput = By.Id("company");
        private readonly By address1Input = By.Id("address1");
        private readonly By address2Input = By.Id("address2");
        private readonly By countryDropdown = By.Id("country");
        private readonly By stateInput = By.Id("state");
        private readonly By cityInput = By.Id("city");
        private readonly By zipcodeInput = By.Id("zipcode");
        private readonly By mobileNumberInput = By.Id("mobile_number");

        // Create Account button
        private readonly By createAccountButton = By.XPath("//button[@data-qa='create-account']");

        public CreateAccountPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public void SelectTitle(string title)
        {
            if (title.Equals("Mr", StringComparison.OrdinalIgnoreCase))
            {
                driver.FindElement(titleMrRadio).Click();
            }
            else if (title.Equals("Mrs", StringComparison.OrdinalIgnoreCase))
            {
                driver.FindElement(titleMrsRadio).Click();
            }
        }

        public void EnterName(string name)
        {
            var element = driver.FindElement(nameInput);
            element.Clear();
            element.SendKeys(name);
        }

        public string GetEmail()
        {
            return driver.FindElement(emailInput).GetAttribute("value");
        }

        public void EnterPassword(string password)
        {
            var element = driver.FindElement(passwordInput);
            element.Clear();
            element.SendKeys(password);
        }

        public void SelectDateOfBirth(string day, string month, string year)
        {
            new SelectElement(driver.FindElement(daysDropdown)).SelectByValue(day);
            // Ensure month has no leading zeros
            month = int.Parse(month).ToString();

            new SelectElement(driver.FindElement(monthsDropdown)).SelectByValue(month);
            new SelectElement(driver.FindElement(yearsDropdown)).SelectByValue(year);
        }

        public void SetNewsletterSubscription(bool subscribe)
        {
            var checkbox = driver.FindElement(newsletterCheckbox);
            if (checkbox.Selected != subscribe)
            {
                checkbox.Click();
            }
        }

        public void SetOffersSubscription(bool subscribe)
        {
            var checkbox = driver.FindElement(offersCheckbox);
            if (checkbox.Selected != subscribe)
            {
                checkbox.Click();
            }
        }

        public void EnterAddressInformation(
            string firstName,
            string lastName,
            string company,
            string address1,
            string address2,
            string country,
            string state,
            string city,
            string zipcode,
            string mobileNumber)
        {
            ClearAndSendKeys(firstNameInput, firstName);
            ClearAndSendKeys(lastNameInput, lastName);
            ClearAndSendKeys(companyInput, company);
            ClearAndSendKeys(address1Input, address1);
            ClearAndSendKeys(address2Input, address2);

            var countrySelect = new SelectElement(driver.FindElement(countryDropdown));
            countrySelect.SelectByText(country);

            ClearAndSendKeys(stateInput, state);
            ClearAndSendKeys(cityInput, city);
            ClearAndSendKeys(zipcodeInput, zipcode);
            ClearAndSendKeys(mobileNumberInput, mobileNumber);
        }

        private void ClearAndSendKeys(By locator, string text)
        {
            var element = driver.FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        public void ClickCreateAccount()
        {
            driver.FindElement(createAccountButton).Click();
        }
    }
}
