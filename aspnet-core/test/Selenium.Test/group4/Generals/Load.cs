using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Selenium.Test.group4.General
{
    public class Load
    {
        private static Load instance;

        public Load() { }


        public IWebDriver driver;
        public string homeURL;

        public const string USERNAME = "admin";
        public const string PASSWORD = "admin";

        public void Login()
        {
            //Arrange
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(homeURL);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            // Act

            //Find Username, Password TextField and send a username, password for login

            driver.FindElement(By.Name("userNameOrEmailAddress")).SendKeys(USERNAME);
            driver.FindElement(By.Name("password")).SendKeys(PASSWORD);

            var signInButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            signInButton.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(e => e.FindElement(By.Id("HostDashboard")));
        }
    }
}
