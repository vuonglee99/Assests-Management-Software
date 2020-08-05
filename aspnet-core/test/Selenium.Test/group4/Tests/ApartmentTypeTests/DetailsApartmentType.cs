using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Test.Helper;
using System.Linq;
using Selenium.Test.group4.DTO;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using Selenium.Test.group4.General;

namespace Selenium.Test.group4.Tests.ApartmentTypeTests
{
    [TestClass]
    public class DetailsApartmentType : Load
    {
        public DetailsApartmentType()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";
        }

        [TestInitialize]
        public void InitializeTest()
        {
            Login();
        }

        [TestCleanup]
        public void CleanTest()
        {
            driver.Close();
        }

        [TestMethod]
        public void Details_ApartmentType_Successfully()
        {
            driver.Navigate().GoToUrl(homeURL + "/app/admin/apartment-type");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //Act
            IReadOnlyCollection<IWebElement> dataRow = driver.FindElements(By.Name("data-row"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            var selectedRow = dataRow.ElementAtOrDefault(0);

            Assert.AreNotEqual(selectedRow, null);
            selectedRow.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            var btnDetail = driver.FindElement(By.Id("btn_detail"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(btnDetail).Click().Perform();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.Url.Contains("/app/admin/apartment-type-view/"));
        }

        [TestMethod]
        public void Details_ApartmentType_FailedByNoID()
        {
            driver.Navigate().GoToUrl(homeURL + "/app/admin/apartment-type");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //Act
            var btnDetail = driver.FindElement(By.Id("btn_detail"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(btnDetail).Click().Perform();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div")).GetAttribute("class").Contains("swal-overlay--show-modal"));
        }
    }
}
