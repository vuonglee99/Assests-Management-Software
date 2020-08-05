using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Test.Helper;
using System.Linq;
using Selenium.Test.group4.DTO;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Collections.Generic;
using Selenium.Test.group4.General;

namespace Selenium.Test.group4.Tests.ApartmentTests
{
    [TestClass]
    public class DetailApartment : Load
    {
        public DetailApartment()
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
        [DataRow("Ap001")]
        public void Detail_Apartment_Successfully(string maCanHo)
        {
            driver.Navigate().GoToUrl(homeURL + "/app/admin/buildings/1/1/apartment");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //Act
            driver.FindElements(By.CssSelector(".group4 .ui-dropdown-label.ui-inputtext.ui-corner-all.ng-star-inserted")).LastOrDefault()?.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            var paginators = driver.FindElements(By.CssSelector(".group4 .ng-star-inserted.ui-dropdown-item.ui-corner-all"));
            paginators.Last().Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IReadOnlyCollection<IWebElement> dataRow = driver.FindElements(By.Name("data-row"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            var selectedClick = dataRow.Where(element =>
            {
                var x = element.FindElements(By.XPath("td"));
                var y = x.ElementAt(1);
                var z = y.Text;
                return z == maCanHo;
            }).FirstOrDefault();

            Assert.AreNotEqual(selectedClick, null);
            selectedClick.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            var btnDetail = driver.FindElement(By.Id("btn_detail"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(btnDetail).Click().Perform();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.Url.Contains("apartment-view/"));
        }

        [TestMethod]
        public void Detail_Apartment_FailedWithNoID()
        {
            driver.Navigate().GoToUrl(homeURL + "/app/admin/buildings/1/1/apartment");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //Act
            var btnDelete = driver.FindElement(By.Id("btn_detail"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(btnDelete).Click().Perform();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            Thread.Sleep(500);
            //Assert
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div")).GetAttribute("class").Contains("swal-overlay--show-modal"));
        }
    }
}
