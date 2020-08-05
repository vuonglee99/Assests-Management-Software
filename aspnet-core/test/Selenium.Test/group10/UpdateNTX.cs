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
    public class UpdateNTX : Load
    {

        public UpdateNTX()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";
        }

        [TestMethod]
        [DataRow("Alana")]
        public void Update_NTX_With_OK_Status(String Name)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Thread.Sleep(8000);

            IWebElement ele0 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor0 = (IJavaScriptExecutor)driver;
            executor0.ExecuteScript("arguments[0].click();", ele0);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            wait.Until(e => e.FindElement(By.Id("ntx_license")).GetAttribute("value") != "");
            //Act

            driver.FindElement(By.Id("ntx_name")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_name")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_name")).SendKeys(Name);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

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
        public void Update_NTX_With_Empty(String Name, String Address, String Code, String CMND, String GPLX)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Thread.Sleep(8000);

            IWebElement ele0 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor0 = (IJavaScriptExecutor)driver;
            executor0.ExecuteScript("arguments[0].click();", ele0);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            wait.Until(e => e.FindElement(By.Id("ntx_license")).GetAttribute("value") != "");

            //Act

            driver.FindElement(By.Id("ntx_name")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_name")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_name")).SendKeys(Name);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("ntx_address")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_address")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_address")).SendKeys(Address);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("ntx_code")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_code")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_code")).SendKeys(Code);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("ntx_card")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_card")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_card")).SendKeys(CMND);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("ntx_license")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_license")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_license")).SendKeys(GPLX);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("ntx_birthday")).Click();
            driver.FindElement(By.Id("ntx_birthday")).SendKeys("02-03-1999");

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
        public void Update_NTX_With_MaxSize(String Name, String Address, String Code, String CMND, String GPLX)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Thread.Sleep(8000);

            IWebElement ele0 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor0 = (IJavaScriptExecutor)driver;
            executor0.ExecuteScript("arguments[0].click();", ele0);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            wait.Until(e => e.FindElement(By.Id("ntx_license")).GetAttribute("value") != "");

            //Act

            driver.FindElement(By.Id("ntx_name")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_name")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_name")).SendKeys(Name);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("ntx_address")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_address")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_address")).SendKeys(Address);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("ntx_code")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_code")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_code")).SendKeys(Code);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("ntx_card")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_card")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_card")).SendKeys(CMND);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("ntx_license")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_license")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_license")).SendKeys(GPLX);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("ntx_birthday")).Click();
            driver.FindElement(By.Id("ntx_birthday")).SendKeys("02-03-1999");

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.Name("maxsize_field")).Displayed);
            driver.Close();
        }

        [TestMethod]
        [DataRow("Alana", "England", "1554423", "22148abc", "1234567890")]
        [DataRow("Alana", "England", "1554423", "221483570", "1abc")]
        public void Update_NTX_With_CheckNumber(String Name, String Address, String Code, String CMND, String GPLX)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Thread.Sleep(8000);

            IWebElement ele0 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor0 = (IJavaScriptExecutor)driver;
            executor0.ExecuteScript("arguments[0].click();", ele0);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            wait.Until(e => e.FindElement(By.Id("ntx_license")).GetAttribute("value") != "");

            //Act
            driver.FindElement(By.Id("ntx_name")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_name")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_name")).SendKeys(Name);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("ntx_address")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_address")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_address")).SendKeys(Address);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("ntx_code")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_code")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_code")).SendKeys(Code);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("ntx_card")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_card")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_card")).SendKeys(CMND);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("ntx_license")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_license")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_license")).SendKeys(GPLX);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.Name("number_field")).Displayed);
            driver.Close();
        }

        [TestMethod]
        public void Update_NTX_With_BirthDate_Future()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Thread.Sleep(8000);

            IWebElement ele0 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor0 = (IJavaScriptExecutor)driver;
            executor0.ExecuteScript("arguments[0].click();", ele0);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            wait.Until(e => e.FindElement(By.Id("ntx_license")).GetAttribute("value") != "");

            //Act
            driver.FindElement(By.Id("ntx_birthday")).Click();
            driver.FindElement(By.Id("ntx_birthday")).SendKeys("02-03-2021");

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.Name("date_error")).Displayed);
            driver.Close();
        }

        [TestMethod]
        [DataRow("1234")]
        public void Update_NTX_With_DuplicateCode(String Code)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Thread.Sleep(8000);

            IWebElement ele0 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor0 = (IJavaScriptExecutor)driver;
            executor0.ExecuteScript("arguments[0].click();", ele0);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            wait.Until(e => e.FindElement(By.Id("ntx_license")).GetAttribute("value") != "");

            driver.FindElement(By.Id("ntx_code")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_code")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("ntx_code")).SendKeys(Code);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            wait.Until(e => e.FindElement(By.CssSelector("body > div > div > div.swal-icon.swal-icon--error")));

            //Assert
            Assert.IsTrue(driver.FindElement(By.CssSelector("body > div > div > div.swal-icon.swal-icon--error")).Displayed);
            driver.Close();
        }

        [TestMethod]
        public void Update_NTX_With_NoData()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Thread.Sleep(8000);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            wait.Until(e => e.FindElement(By.CssSelector("body > div > div > div.swal-icon.swal-icon--error")));

            //Assert
            Assert.IsTrue(driver.FindElement(By.CssSelector("body > div > div > div.swal-icon.swal-icon--error")).Displayed);
            driver.Close();
        }

    }
}
