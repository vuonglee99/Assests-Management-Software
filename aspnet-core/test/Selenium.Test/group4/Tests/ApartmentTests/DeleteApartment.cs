using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using Selenium.Test.group4.DTO;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using Selenium.Test.Helper;
using Selenium.Test.group4.General;

namespace Selenium.Test.group4.Tests.ApartmentTests
{
    [TestClass]
    public class DeleteApartment : Load
    {
        public DeleteApartment()
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
            ApartmentDTO dto = new ApartmentDTO();
            dto.APARTMENT_CODE = "Ap008";
            dto.APARTMENT_NAME = "Căn hộ 08";
            dto.APARTMENT_TYPE_ID = "ApartmentType0000001";
            dto.BUILDING_ID = "BUILDING000000000032";
            dto.Floor_ID = "Floor000000000000033";
            dto.APARTMENT_ROOMS = 4;
            //dto.Apartment_ow = "Tung";
            dto.APARTMENT_PRICE = 5000000;
            dto.APARTMENT_STATUS = "0";
            dto.APARTMENT_AREA = 200;
            DataProvider.Instance.GetData<dynamic>("Apartment_Insert", dto);
        }

        [TestMethod]
        [DataRow("Ap008")]
        public void Delete_Apartment_Successfully(string maCanHo)
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

            var deletedClick = dataRow.Where(element =>
            {
                var x = element.FindElements(By.XPath("td"));
                var y = x.ElementAt(1);
                var z = y.Text;
                return z == maCanHo;
            }).FirstOrDefault();

            Assert.AreNotEqual(deletedClick, null);
            deletedClick.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            var btnDelete = driver.FindElement(By.Id("btn_delete"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(btnDelete).Click().Perform();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            Thread.Sleep(300);
            driver.FindElement(By.XPath("/html/body/div/div/div[4]/div[2]/button")).Click();
            Thread.Sleep(300);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            //Assert
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div/div/div")).GetAttribute("class").Contains("swal-icon--success"));
        }

        [TestMethod]
        [DataRow("Ap001")]
        public void Delete_ApartmentType_FailedBczAlreadyApproved(string maCanHo)
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

            var deletedClick = dataRow.Where(element =>
            {
                var x = element.FindElements(By.XPath("td"));
                var y = x.ElementAt(1);
                var z = y.Text;
                return z == maCanHo;
            }).FirstOrDefault();

            Assert.AreNotEqual(deletedClick, null);
            deletedClick.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            var btnDelete = driver.FindElement(By.Id("btn_delete"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(btnDelete).Click().Perform();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            Thread.Sleep(300);
            driver.FindElement(By.XPath("/html/body/div/div/div[4]/div[2]/button")).Click();
            Thread.Sleep(300);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            //Assert
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div/div/div")).GetAttribute("class").Contains("swal-icon--error"));
        }

        [TestMethod]
        public void Delete_Apartment_FailedWithNoID()
        {
            driver.Navigate().GoToUrl(homeURL + "/app/admin/buildings/1/1/apartment");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //Act
            var btnDelete = driver.FindElement(By.Id("btn_delete"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(btnDelete).Click().Perform();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            Thread.Sleep(500);
            //Assert
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div")).GetAttribute("class").Contains("swal-overlay--show-modal"));
        }

    }
}
