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
    public class UpdateApartmentType : Load
    {
        public UpdateApartmentType()
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
                APARTMENT_TYPE_CODE = "Ap060",
                APARTMENT_TYPE_NAME = "Căn hộ penthouse Updated",
            }).FirstOrDefault();

            if (row != null)
            {
                row.APARTMENT_TYPE_CODE = "Ap006";
                row.APARTMENT_TYPE_NAME = "Căn hộ penthouse";
                row.APARTMENT_TYPE_DESCRIPTION = "Thiết kế căn hộ penthouse thường theo hướng không gian mở nhằm tận dụng lợi thế vị trí trên cao của mình, mở rộng tối đa tầm nhìn cho gia chủ. Đồng thời, bên trong penthouse hạn chế sử dụng vách ngăn";
                DataProvider.Instance.GetData<ApartmentTypeDTO>("ApartmentType_Update", row);
            }
        }

        [TestMethod]
        [DataRow("Ap006", "Ap060", "Căn hộ penthouse Updated", "Description Updated")]
        public void Update_ApartmentType_Successfully(string code, string newCode, string newName, string newDescription)
        {
            driver.Navigate().GoToUrl(homeURL + "/app/admin/apartment-type");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //Act
            //click 50
            driver.FindElement(By.CssSelector(".group4 .ui-dropdown-label.ui-inputtext.ui-corner-all.ng-star-inserted"))?.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            var paginators = driver.FindElements(By.CssSelector(".group4 .ng-star-inserted.ui-dropdown-item.ui-corner-all"));
            paginators.Last().Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //click row
            IReadOnlyCollection<IWebElement> dataRow = driver.FindElements(By.Name("data-row"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            var selectedRow = dataRow.Where(element =>
            {
                var x = element.FindElements(By.XPath("td"));
                var y = x.ElementAt(1);
                var z = y.Text;
                return z == code;
            }).FirstOrDefault();

            Assert.AreNotEqual(selectedRow, null);
            selectedRow.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //click update
            var btnUpdate = driver.FindElement(By.Id("btn_update"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(btnUpdate).Click().Perform();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Send data
            driver.FindElement(By.Id("ap_type_code")).Clear();
            driver.FindElement(By.Id("ap_type_code")).SendKeys(newCode);

            driver.FindElement(By.Id("ap_type_name")).Clear();
            driver.FindElement(By.Id("ap_type_name")).SendKeys(newName);

            driver.FindElement(By.Id("ap_type_description")).Clear();
            driver.FindElement(By.Id("ap_type_description")).SendKeys(newDescription);

            driver.FindElement(By.Id("btn_save")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div/div/div")).GetAttribute("class").Contains("swal-icon--success"));
        }

        [TestMethod]
        [DataRow("Ap006", "", "Căn hộ penthouse Updated", "Description Updated")]
        [DataRow("Ap006", "Ap060", "", "Description Updated")]
        [DataRow("Ap006", "", "", "Description Updated")]
        public void Update_ApartmentType_FailedWithEmpty(string code, string newCode, string newName, string newDescription)
        {
            driver.Navigate().GoToUrl(homeURL + "/app/admin/apartment-type");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //Act
            //click 50
            driver.FindElement(By.CssSelector(".group4 .ui-dropdown-label.ui-inputtext.ui-corner-all.ng-star-inserted"))?.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            var paginators = driver.FindElements(By.CssSelector(".group4 .ng-star-inserted.ui-dropdown-item.ui-corner-all"));
            paginators.Last().Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //click row
            IReadOnlyCollection<IWebElement> dataRow = driver.FindElements(By.Name("data-row"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            var selectedRow = dataRow.Where(element =>
            {
                var x = element.FindElements(By.XPath("td"));
                var y = x.ElementAt(1);
                var z = y.Text;
                return z == code;
            }).FirstOrDefault();

            Assert.AreNotEqual(selectedRow, null);
            selectedRow.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //click update
            var btnUpdate = driver.FindElement(By.Id("btn_update"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(btnUpdate).Click().Perform();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Send data
            driver.FindElement(By.Id("ap_type_code")).Clear();
            driver.FindElement(By.Id("ap_type_code")).SendKeys(newCode);

            driver.FindElement(By.Id("ap_type_name")).Clear();
            driver.FindElement(By.Id("ap_type_name")).SendKeys(newName);

            driver.FindElement(By.Id("ap_type_description")).Clear();
            driver.FindElement(By.Id("ap_type_description")).SendKeys(newDescription);

            driver.FindElement(By.Id("btn_save")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div")).GetAttribute("class").Contains("swal-overlay--show-modal"));
        }

        [TestMethod]
        [DataRow("Ap006", "Ap001", "Penthouse Updated", "Description Updated")]
        public void Update_ApartmentType_FailedExists(string code, string newCode, string newName, string newDescription)
        {
            driver.Navigate().GoToUrl(homeURL + "/app/admin/apartment-type");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //Act
            //click 50
            driver.FindElement(By.CssSelector(".group4 .ui-dropdown-label.ui-inputtext.ui-corner-all.ng-star-inserted"))?.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            var paginators = driver.FindElements(By.CssSelector(".group4 .ng-star-inserted.ui-dropdown-item.ui-corner-all"));
            paginators.Last().Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //click row
            IReadOnlyCollection<IWebElement> dataRow = driver.FindElements(By.Name("data-row"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            var selectedRow = dataRow.Where(element =>
            {
                var x = element.FindElements(By.XPath("td"));
                var y = x.ElementAt(1);
                var z = y.Text;
                return z == code;
            }).FirstOrDefault();

            Assert.AreNotEqual(selectedRow, null);
            selectedRow.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //click update
            var btnUpdate = driver.FindElement(By.Id("btn_update"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(btnUpdate).Click().Perform();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Send data
            driver.FindElement(By.Id("ap_type_code")).Clear();
            driver.FindElement(By.Id("ap_type_code")).SendKeys(newCode);

            driver.FindElement(By.Id("ap_type_name")).Clear();
            driver.FindElement(By.Id("ap_type_name")).SendKeys(newName);

            driver.FindElement(By.Id("ap_type_description")).Clear();
            driver.FindElement(By.Id("ap_type_description")).SendKeys(newDescription);

            driver.FindElement(By.Id("btn_save")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div")).GetAttribute("class").Contains("swal-overlay--show-modal"));
        }
    }
}
