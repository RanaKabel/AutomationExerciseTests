// File: SignupLoginPage.cs
using OpenQA.Selenium;

namespace AutomationExerciseTests.PageObjects
{
    public class SignupLoginPage
    {
        private readonly IWebDriver driver;

        // Locators for signup
        private readonly By nameInput = By.XPath("//input[@data-qa='signup-name']");
        private readonly By emailSignupInput = By.XPath("//input[@data-qa='signup-email']");
        private readonly By signupButton = By.XPath("//button[@data-qa='signup-button']");

        // Locators for login
        private readonly By emailLoginInput = By.XPath("//input[@data-qa='login-email']");
        private readonly By passwordLoginInput = By.XPath("//input[@data-qa='login-password']");
        private readonly By loginButton = By.XPath("//button[@data-qa='login-button']");

        // Error message locator
        private readonly By errorMessage = By.XPath("//p[@style='color: red;']");

        public SignupLoginPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public void RegisterUser(string name, string email)
        {
            driver.FindElement(nameInput).Clear();
            driver.FindElement(nameInput).SendKeys(name);

            driver.FindElement(emailSignupInput).Clear();
            driver.FindElement(emailSignupInput).SendKeys(email);

            driver.FindElement(signupButton).Click();

            // You should now be redirected to the next signup page where the password is set
            // Wait and interact with that page in another PageObject if needed
        }

        public void LoginUser(string email, string password)
        {
            driver.FindElement(emailLoginInput).Clear();
            driver.FindElement(emailLoginInput).SendKeys(email);

            driver.FindElement(passwordLoginInput).Clear();
            driver.FindElement(passwordLoginInput).SendKeys(password);

            driver.FindElement(loginButton).Click();
        }

        public string GetErrorMessage()
        {
            try
            {
                return driver.FindElement(errorMessage).Text.Trim();
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }
    }
}
