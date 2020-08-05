using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace Selenium.Test.group10
{
    [TestClass]
    public class ShowDetailNSX : Load
    {
        public ShowDetailNSX()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        public void Show_Detail_NSX_With_OK_Status()
        {
            Login();


            driver.Navigate().GoToUrl(homeURL + "/app/admin/nsx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(5000);

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
            wait.Until(e => e.FindElement(By.Id("nsx_code")).GetAttribute("value"));
            Assert.AreNotEqual("", driver.FindElement(By.Id("nsx_code")).GetAttribute("value"));

            driver.Close();
        }

        /*[TestMethod]
        public void Show_Detail_NSX_With_No_ID()
        {
            Login();


            driver.Navigate().GoToUrl(homeURL + "/app/admin/nsx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //Act

            driver.FindElement(By.Id("btnShow")).Click();


            //Assert
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div")).GetAttribute("class").Contains("swal-overlay--show-modal"));
        }*/
    }
}
