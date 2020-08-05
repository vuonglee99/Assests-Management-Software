using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Test.Helper;
using System.Linq;
using Selenium.Test.group4.DTO;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using System.Threading;
using Selenium.Test.group4.General;

namespace Selenium.Test.group4.Tests.ApartmentTypeTests
{
    [TestClass]
    public class DeleteApartmentType : Load
    {
        public DeleteApartmentType()
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
            var row = DataProvider.Instance.GetData<ApartmentTypeDTO>("ApartmentType_Search", new
            {
                APARTMENT_TYPE_CODE = "Ap006",
                APARTMENT_TYPE_NAME = "Căn hộ penthouse",
            }).FirstOrDefault();
            if (row == null)
                DataProvider.Instance.GetData<dynamic>("ApartmentType_Insert", new ApartmentTypeDTO("Ap006", "Căn hộ penthouse", "Thiết kế căn hộ penthouse thường theo hướng không gian mở nhằm tận dụng lợi thế vị trí trên cao của mình, mở rộng tối đa tầm nhìn cho gia chủ. Đồng thời, bên trong penthouse hạn chế sử dụng vách ngăn"));
        }

        [TestMethod]
        [DataRow("Ap006")]
        public void Delete_ApartmentType_Successful(string delCode)
        {
            driver.Navigate().GoToUrl(homeURL + "/app/admin/apartment-type");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //Act
            driver.FindElement(By.CssSelector(".group4 .ui-dropdown-label.ui-inputtext.ui-corner-all.ng-star-inserted"))?.Click();
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
                return z == delCode;
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
        [DataRow("Ap006")]
        public void Delete_ApartmentType_FailedByCancel(string code)
        {
            driver.Navigate().GoToUrl(homeURL + "/app/admin/apartment-type");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //Act
            driver.FindElement(By.CssSelector(".group4 .ui-dropdown-label.ui-inputtext.ui-corner-all.ng-star-inserted"))?.Click();
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
                return z == code;
            }).FirstOrDefault();

            Assert.AreNotEqual(deletedClick, null);
            deletedClick.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            var btnDelete = driver.FindElement(By.Id("btn_delete"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(btnDelete).Click().Perform();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            Thread.Sleep(300);
            driver.FindElement(By.XPath("/html/body/div/div/div[4]/div[1]/button")).Click();
            Thread.Sleep(300);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsFalse(driver.FindElement(By.XPath("/html/body/div/div/div")).GetAttribute("class").Contains("swal-icon--success"));
        }

        [TestMethod]
        public void Delete_ApartmentType_FailedWithNoID()
        {
            driver.Navigate().GoToUrl(homeURL + "/app/admin/apartment-type");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //Act
            var btnDelete = driver.FindElement(By.Id("btn_delete"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(btnDelete).Click().Perform();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div")).GetAttribute("class").Contains("swal-overlay--show-modal"));
        }

        [TestMethod]
        [DataRow("Ap001")]
        public void Delete_ApartmentType_FailedByBeingUsed(string code)
        {
            driver.Navigate().GoToUrl(homeURL + "/app/admin/apartment-type");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //Act
            driver.FindElement(By.CssSelector(".group4 .ui-dropdown-label.ui-inputtext.ui-corner-all.ng-star-inserted"))?.Click();
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
                return z == code;
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
    }
}
