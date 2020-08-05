using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Test.Helper;
using System.Linq;
using Selenium.Test.NSX_DTO;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace Selenium.Test.group10
{
    [TestClass]
    public class CreateNSX : Load
    {

        public CreateNSX()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";
            
        }

        [TestMethod]
        [DataRow("Yamaha", "Japan")]
        public void Create_NSX_With_OK_Status(String Name, String Origin)
        {
            String Code = (new Random().Next(1, 99999999)).ToString();
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/nsx-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Id("nsx_code")));
            Thread.Sleep(8000);

            //Act;
            driver.FindElement(By.Id("nsx_code")).SendKeys(Code);
            driver.FindElement(By.Id("nsx_name")).SendKeys(Name);
            driver.FindElement(By.Id("nsx_from")).SendKeys(Origin);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            //Check with value of DB
            //CM_NSX_DTO input = new CM_NSX_DTO(Code, Name, Origin);
            CM_NSX_DTO finalRow = DataProvider.Instance.GetData<CM_NSX_DTO>("NhaSanXuat_Search", new { NSX_CODE = Code, RECORD_STATUS = 1 }). ToList().FirstOrDefault();

            //Assert
            Assert.AreEqual(Code, finalRow.NSX_CODE);
            Assert.AreEqual(Name, finalRow.NSX_NAME);
            Assert.AreEqual(Origin, finalRow.NSX_FROM);

            driver.Close();

        }

        [TestMethod]
        [DataRow("", "Yamaha", "Japan")]
        [DataRow("12344321", "", "Japan")]
        [DataRow("123443212", "Yamaha", "")]
        public void Create_NSX_With_Empty(String Code, String Name, String Origin)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/nsx-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Id("nsx_code")));
            Thread.Sleep(8000);

            //Act

            driver.FindElement(By.Id("nsx_code")).SendKeys(Code);
            driver.FindElement(By.Id("nsx_name")).SendKeys(Name);
            driver.FindElement(By.Id("nsx_from")).SendKeys(Origin);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.Name("empty_field")).Displayed);
            driver.Close();
        }

        [TestMethod]
        [DataRow("111", "Yamahazeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee", "Japanz")]
        [DataRow("222222222222222222222", "SH", "Japanz")]
        [DataRow("1112", "SH", "Vietnameseeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee")]
        public void Create_NSX_With_MaxSize(String Code, String Name, String Origin)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/nsx-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Id("nsx_code")));
            Thread.Sleep(8000);

            //Act
            driver.FindElement(By.Id("nsx_code")).SendKeys(Code);
            driver.FindElement(By.Id("nsx_name")).SendKeys(Name);
            driver.FindElement(By.Id("nsx_from")).SendKeys(Origin);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.Name("maxsize_field")).Displayed);
            driver.Close();
        }


        [TestMethod]
        [DataRow("123", "Yamaha", "Japan")]
        
        public void Create_NSX_With_DuplicateCode(String Code, String Name, String Origin)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/nsx-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Id("nsx_code")));
            Thread.Sleep(8000);

            //Act
            driver.FindElement(By.Id("nsx_code")).SendKeys(Code);
            driver.FindElement(By.Id("nsx_name")).SendKeys(Name);
            driver.FindElement(By.Id("nsx_from")).SendKeys(Origin);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            wait.Until(e => e.FindElement(By.CssSelector("body > div > div > div.swal-icon.swal-icon--error")));

            //Assert
            Assert.IsTrue(driver.FindElement(By.CssSelector("body > div > div > div.swal-icon.swal-icon--error")).Displayed);
            driver.Close();
        }

    }
}
