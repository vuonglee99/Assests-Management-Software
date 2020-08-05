using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Test.Helper;
using System.Linq;
using Selenium.Test.NSX_DTO;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Threading;

namespace Selenium.Test.group10
{
    [TestClass]
    public class UpdateNSX : Load
    {
        public UpdateNSX()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        public void Update_NSX_With_OK_Status()
        {
            String Name = "SYM " + new Random().Next().ToString();
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/nsx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[2]/div[2]/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[3]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Id("nsx_name")).GetAttribute("value") != "");

            IWebElement eleEdit = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]"));
            IJavaScriptExecutor executorEdit = (IJavaScriptExecutor)driver;
            executorEdit.ExecuteScript("arguments[0].click();", eleEdit);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            //Act;
            driver.FindElement(By.Id("nsx_name")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("nsx_name")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            wait.Until(e => e.FindElement(By.Id("nsx_name")).GetAttribute("value") == "");
            driver.FindElement(By.Id("nsx_name")).SendKeys(Name);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnSaveNTX")));
            IWebElement eleUpdate = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]"));
            IJavaScriptExecutor executorUpdate = (IJavaScriptExecutor)driver;
            executorUpdate.ExecuteScript("arguments[0].click();", eleUpdate);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            String Code = driver.FindElement(By.Id("nsx_code")).GetAttribute("value");
            //Check with value of DB
            CM_NSX_DTO finalRow = DataProvider.Instance.GetData<CM_NSX_DTO>("NhaSanXuat_Search", new { NSX_CODE = Code, RECORD_STATUS = 1 }).ToList().FirstOrDefault();

            //Assert
            Assert.AreEqual(Name, finalRow.NSX_NAME);

            driver.Close();
        }

        [TestMethod]
        [DataRow("", "SYM", "Korea")]
        [DataRow("11234950", "", "Korea")]
        [DataRow("11234950", "SYM", "")]
        public void Update_NSX_With_Empty(String Code, String Name, String Origin)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/nsx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[2]/div[2]/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[3]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Id("nsx_name")).GetAttribute("value") != "");

            IWebElement eleEdit = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]"));
            IJavaScriptExecutor executorEdit = (IJavaScriptExecutor)driver;
            executorEdit.ExecuteScript("arguments[0].click();", eleEdit);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);


            //Act
            driver.FindElement(By.Id("nsx_code")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("nsx_code")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("nsx_code")).SendKeys(Code);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("nsx_name")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("nsx_name")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("nsx_name")).SendKeys(Name);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("nsx_from")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("nsx_from")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("nsx_from")).SendKeys(Origin);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleUpdate = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]"));
            IJavaScriptExecutor executorUpdate = (IJavaScriptExecutor)driver;
            executorUpdate.ExecuteScript("arguments[0].click();", eleUpdate);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            //Assert
            Assert.IsTrue(driver.FindElement(By.Name("empty_field")).Displayed);
            driver.Close();
        }

        [TestMethod]
        [DataRow("11234950", "Yamahazeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee", "Korea")]
        [DataRow("222222222222222222222", "SYMM", "Korea")]
        [DataRow("11234950", "SYMM", "Vietnameseeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee")]
        public void Update_NSX_With_MaxSize(String Code, String Name, String Origin)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/nsx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[2]/div[2]/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[3]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Id("nsx_name")).GetAttribute("value") != "");

            IWebElement eleEdit = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]"));
            IJavaScriptExecutor executorEdit = (IJavaScriptExecutor)driver;
            executorEdit.ExecuteScript("arguments[0].click();", eleEdit);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            //Act
            driver.FindElement(By.Id("nsx_code")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("nsx_code")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("nsx_code")).SendKeys(Code);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("nsx_name")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("nsx_name")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("nsx_name")).SendKeys(Name);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("nsx_from")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("nsx_from")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("nsx_from")).SendKeys(Origin);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            IWebElement eleUpdate = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]"));
            IJavaScriptExecutor executorUpdate = (IJavaScriptExecutor)driver;
            executorUpdate.ExecuteScript("arguments[0].click();", eleUpdate);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            //Assert
            Assert.IsTrue(driver.FindElement(By.Name("maxsize_field")).Displayed);
            driver.Close();

        }
    }
}
