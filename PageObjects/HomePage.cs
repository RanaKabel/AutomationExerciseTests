using OpenQA.Selenium;

namespace AutomationExerciseTests.PageObjects
{
    public class HomePage
    {
        private readonly IWebDriver driver;

        private readonly By signupLoginLink = By.XPath("//a[contains(text(), 'Signup / Login')]");

        public HomePage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public void NavigateToHomePage()
        {
            driver.Navigate().GoToUrl("https://www.automationexercise.com");
        }

        public void ClickSignupLogin()
        {
            driver.FindElement(signupLoginLink).Click();
        }
    }
}
