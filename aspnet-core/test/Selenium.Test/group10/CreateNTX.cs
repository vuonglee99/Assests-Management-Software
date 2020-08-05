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
    public class CreateNTX : Load
    {

        public CreateNTX()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";
        }

        [TestMethod]
        [DataRow("Alana", "England", "221483570", "1234567890")]
        public void Create_NTX_With_OK_Status(String Name, String Address, String CMND, String GPLX)
        {
            String Code = (new Random().Next(1, 99999999)).ToString();
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Thread.Sleep(8000);

            //Act

            driver.FindElement(By.Id("ntx_name")).SendKeys(Name);
            driver.FindElement(By.Id("ntx_address")).SendKeys(Address);
            driver.FindElement(By.Id("ntx_code")).SendKeys(Code);
            driver.FindElement(By.Id("ntx_birthday")).Click();
            driver.FindElement(By.Id("ntx_birthday")).SendKeys("02-03-1999");
            driver.FindElement(By.Id("ntx_card")).SendKeys(CMND);
            driver.FindElement(By.Id("ntx_license")).SendKeys(GPLX);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Check with value of DB
            //CM_NSX_DTO input = new CM_NSX_DTO(Code, Name, Origin);
            wait.Until(e => e.FindElement(By.CssSelector("body > div > div > div.swal-icon.swal-icon--success > div.swal-icon--success__ring")));

            //Assert
            Assert.IsTrue(driver.FindElement(By.CssSelector("body > div > div > div.swal-icon.swal-icon--success > div.swal-icon--success__ring")).Displayed);
            driver.Close();

        }

        [TestMethod]
        [DataRow("", "England", "1554423", "221483570", "1234567890")]
        [DataRow("Alana", "", "1554423", "221483570", "1234567890")]
        [DataRow("Alana", "England", "", "221483570", "1234567890")]
        [DataRow("Alana", "England", "1554423", "", "1234567890")]
        [DataRow("Alana", "England", "1554423", "221483570", "")]
        public void Create_NTX_With_Empty(String Name, String Address, String Code, String CMND, String GPLX)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Thread.Sleep(8000);

            //Act

            driver.FindElement(By.Id("ntx_name")).SendKeys(Name);
            driver.FindElement(By.Id("ntx_address")).SendKeys(Address);
            driver.FindElement(By.Id("ntx_code")).SendKeys(Code);
            driver.FindElement(By.Id("ntx_birthday")).Click();
            driver.FindElement(By.Id("ntx_birthday")).SendKeys("02-03-1999");
            driver.FindElement(By.Id("ntx_card")).SendKeys(CMND);
            driver.FindElement(By.Id("ntx_license")).SendKeys(GPLX);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.Name("empty_field")).Displayed);
            driver.Close();
        }

        [TestMethod]
        [DataRow("Alanatttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt", "England", "1554423", "221483570", "1234567890")]
        [DataRow("Alana", "Englandtttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt", "1554423", "221483570", "1234567890")]
        [DataRow("Alana", "England", "11111111111111111111111111111111111111", "221483570", "1234567890")]
        [DataRow("Alana", "England", "1554423", "1111111111", "1234567890")]
        [DataRow("Alana", "England", "1554423", "221483570", "111111111111111111111111111111")]
        public void Create_NTX_With_MaxSize(String Name, String Address, String Code, String CMND, String GPLX)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Thread.Sleep(8000);

            //Act

            driver.FindElement(By.Id("ntx_name")).SendKeys(Name);
            driver.FindElement(By.Id("ntx_address")).SendKeys(Address);
            driver.FindElement(By.Id("ntx_code")).SendKeys(Code);
            driver.FindElement(By.Id("ntx_birthday")).Click();
            driver.FindElement(By.Id("ntx_birthday")).SendKeys("02-03-1999");
            driver.FindElement(By.Id("ntx_card")).SendKeys(CMND);
            driver.FindElement(By.Id("ntx_license")).SendKeys(GPLX);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.Name("maxsize_field")).Displayed);
            driver.Close();
        }

        [TestMethod]
        [DataRow("Alana", "England", "1554423", "22148abc", "1234567890")]
        [DataRow("Alana", "England", "1554423", "221483570", "1abc")]
        public void Create_NTX_With_CheckNumber(String Name, String Address, String Code, String CMND, String GPLX)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Thread.Sleep(8000);

            //Act

            driver.FindElement(By.Id("ntx_name")).SendKeys(Name);
            driver.FindElement(By.Id("ntx_address")).SendKeys(Address);
            driver.FindElement(By.Id("ntx_code")).SendKeys(Code);
            driver.FindElement(By.Id("ntx_birthday")).Click();
            driver.FindElement(By.Id("ntx_birthday")).SendKeys("02-03-1999");
            driver.FindElement(By.Id("ntx_card")).SendKeys(CMND);
            driver.FindElement(By.Id("ntx_license")).SendKeys(GPLX);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.Name("number_field")).Displayed);
            driver.Close();
        }

        [TestMethod]
        [DataRow("Alana", "England", "1554423", "221483574", "1234567890")]
        public void Create_NTX_With_BirthDate_Future(String Name, String Address, String Code, String CMND, String GPLX)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Thread.Sleep(8000);

            //Act

            driver.FindElement(By.Id("ntx_name")).SendKeys(Name);
            driver.FindElement(By.Id("ntx_address")).SendKeys(Address);
            driver.FindElement(By.Id("ntx_code")).SendKeys(Code);
            driver.FindElement(By.Id("ntx_card")).SendKeys(CMND);
            driver.FindElement(By.Id("ntx_license")).SendKeys(GPLX);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.Name("date_error")).Displayed);
            driver.Close();
        }

        [TestMethod]
        [DataRow("Alana", "England", "1234", "221483574", "1234567890")]
        public void Create_NTX_With_DuplicateCode(String Name, String Address, String Code, String CMND, String GPLX)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Thread.Sleep(8000);

            //Act

            driver.FindElement(By.Id("ntx_name")).SendKeys(Name);
            driver.FindElement(By.Id("ntx_address")).SendKeys(Address);
            driver.FindElement(By.Id("ntx_code")).SendKeys(Code);
            driver.FindElement(By.Id("ntx_birthday")).Click();
            driver.FindElement(By.Id("ntx_birthday")).SendKeys("02-03-1999");
            driver.FindElement(By.Id("ntx_card")).SendKeys(CMND);
            driver.FindElement(By.Id("ntx_license")).SendKeys(GPLX);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            wait.Until(e => e.FindElement(By.CssSelector("body > div > div > div.swal-icon.swal-icon--error")));

            //Assert
            Assert.IsTrue(driver.FindElement(By.CssSelector("body > div > div > div.swal-icon.swal-icon--error")).Displayed);
            driver.Close();
        }

    }
}
