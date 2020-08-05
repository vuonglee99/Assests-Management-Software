using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Test.Helper;
using System.Linq;
using Selenium.Test.group4.DTO;
using OpenQA.Selenium.Support.UI;
using Selenium.Test.group4.General;

namespace Selenium.Test.group4.Tests.ApartmentTypeTests
{
    [TestClass]
    public class CreateApartmentType : Load
    {

        public CreateApartmentType()
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
                APARTMENT_TYPE_CODE = "Ap100",
                APARTMENT_TYPE_NAME = "Căn hộ pousethen",
            }).FirstOrDefault();
            if (row != null)
                DataProvider.Instance.GetData<ApartmentTypeDTO>("ApartmentType_Delete", row);
        }

        [TestMethod]
        [DataRow("Ap100", "Căn hộ pousethen", "Thiết kế căn hộ pousethen thường theo hướng không gian mở nhằm tận dụng lợi thế vị trí trên cao của mình, mở rộng tối đa tầm nhìn cho gia chủ. Đồng thời, bên trong pousethen hạn chế sử dụng vách ngăn")]
        public void Create_ApartmentType_CreateSuccessfully(string code, string name, string description)
        {
            driver.Navigate().GoToUrl(homeURL + "/app/admin/apartment-type-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Act
            driver.FindElement(By.Id("ap_type_code")).SendKeys(code);
            driver.FindElement(By.Id("ap_type_name")).SendKeys(name);
            driver.FindElement(By.Id("ap_type_description")).SendKeys(description);

            driver.FindElement(By.Id("btn_save")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Check with value of DB
            //ApartmentTypeDTO input = new ApartmentTypeDTO(code, name, description);
            //ApartmentTypeDTO finalRow = DataProvider.Instance.GetData<ApartmentTypeDTO>("ApartmentType_ByCode", new
            //{
            //    APARTMENT_TYPE_CODE = code
            //}).ToList().FirstOrDefault();

            //Assert
            //Assert.AreNotEqual(finalRow, null);
            //Assert.AreEqual(code, finalRow.APARTMENT_TYPE_CODE);
            //Assert.AreEqual(name, finalRow.APARTMENT_TYPE_NAME);
            //Assert.AreEqual(description, finalRow.APARTMENT_TYPE_DESCRIPTION);
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div/div/div")).GetAttribute("class").Contains("swal-icon--success"));
        }

        [TestMethod]
        [DataRow("Ap001", "Căn hộ sang chảnh", "Căn hộ sang chảnh là khái niệm mới mẽ trong những năm trở lại đây. Thuật ngữ sang chảnh là sự kết hợp giữa căn hộ - văn phòng – khách sạn trong tiếng Anh. Do đó, căn hộ sang chảnh cũng có thể đảm nhiệm n")]
        public void Create_ApartmentType_CreateFailedByExistingCode(string code, string name, string description)
        {
            driver.Navigate().GoToUrl(homeURL + "/app/admin/apartment-type-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Act
            driver.FindElement(By.Id("ap_type_code")).SendKeys(code);
            driver.FindElement(By.Id("ap_type_name")).SendKeys(name);
            driver.FindElement(By.Id("ap_type_description")).SendKeys(description);

            driver.FindElement(By.Id("btn_save")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div/div/div")).GetAttribute("class").Contains("swal-icon--error"));
        }

        [TestMethod]
        [DataRow("", "Namea43fk", "Description")]
        [DataRow("Codea43fk", "", "Description")]
        [DataRow("", "", "Description")]
        public void Create_ApartmentType_FailedByEmptyCodeOrName(string code, string name, string description)
        {
            driver.Navigate().GoToUrl(homeURL + "/app/admin/apartment-type-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Act

            driver.FindElement(By.Id("ap_type_code")).SendKeys(code);
            driver.FindElement(By.Id("ap_type_name")).SendKeys(name);
            driver.FindElement(By.Id("ap_type_description")).SendKeys(description);

            driver.FindElement(By.Id("btn_save")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div/div/div")).GetAttribute("class").Contains("swal-icon--error"));
            //TODO ASSERT EMPTY FIELD
        }
    }
}
