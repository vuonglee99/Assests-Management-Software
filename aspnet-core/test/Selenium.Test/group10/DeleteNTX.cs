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
    public class DeleteNTX : Load
    {
        public DeleteNTX()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        public void Delete_NTX_With_OK_Status()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[3]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleYes = driver.FindElement(By.CssSelector("body > div > div > div.swal-footer > div:nth-child(2) > button"));
            IJavaScriptExecutor executorYes = (IJavaScriptExecutor)driver;
            executorYes.ExecuteScript("arguments[0].click();", eleYes);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            //Check with value of DB

            Assert.IsTrue(driver.FindElement(By.CssSelector("body > div > div > div.swal-icon.swal-icon--success > div.swal-icon--success__ring")).Displayed);
            driver.Close();
        }

        [TestMethod]
        public void Cancel_Delete_NTX()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[3]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleYes = driver.FindElement(By.CssSelector("body > div > div > div.swal-footer > div:nth-child(2) > button"));
            IJavaScriptExecutor executorYes = (IJavaScriptExecutor)driver;
            executorYes.ExecuteScript("arguments[0].click();", eleYes);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            driver.Close();
        }

        [TestMethod]
        public void Delete_NTX_With_NoData()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            Thread.Sleep(8000);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[3]"));
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
