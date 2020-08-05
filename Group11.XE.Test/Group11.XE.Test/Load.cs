using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group11.XE.Test
{
    public class Load
    {
        private static Load instance;

        public Load() { }


        public IWebDriver driver;
        public string homeURL;

        public const string USERNAME = "admin";
        public const string PASSWORD = "123qwe";

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
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Id("HostDashboard")));
        }
    }
}
