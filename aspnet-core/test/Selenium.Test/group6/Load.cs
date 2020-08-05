using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Test.group6
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

            WebDriverWait wait4 = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait4.Until(e => e.FindElement(By.Name("userNameOrEmailAddress")));

            // Act

            //Find Username, Password TextField and send a username, password for login

            driver.FindElement(By.Name("userNameOrEmailAddress")).SendKeys(USERNAME);
            driver.FindElement(By.Name("password")).SendKeys(PASSWORD);

            var signInButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            signInButton.Click();
           
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[1]/side-bar-menu/nav/div[1]/div/ul/li[4]/nav/ul/li[4]/a/span")));
                                                     
        }
    }
}
