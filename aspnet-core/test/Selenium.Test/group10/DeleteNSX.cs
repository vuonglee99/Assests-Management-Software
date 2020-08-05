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
    public class DeleteNSX : Load
    {
        public DeleteNSX()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        public void Delete_NSX_With_OK_Status()
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

            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[2]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleYes = driver.FindElement(By.CssSelector("body > div > div > div.swal-footer > div:nth-child(2) > button"));
            IJavaScriptExecutor executorYes = (IJavaScriptExecutor)driver;
            executorYes.ExecuteScript("arguments[0].click();", eleYes);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            String Code = driver.FindElement(By.Id("nsx_code")).GetAttribute("value");
            //Check with value of DB
            CM_NSX_DTO finalRow = DataProvider.Instance.GetData<CM_NSX_DTO>("NhaSanXuat_Search", new { NSX_CODE = Code, RECORD_STATUS = 1 }).ToList().FirstOrDefault();

            //Assert

            Assert.IsNull(finalRow);
            driver.Close();

        }

        [TestMethod]
        public void Cancel_Delete_NSX()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/nsx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            //Act
            Thread.Sleep(8000);

            //Act
            //IReadOnlyCollection<IWebElement> dataRow = driver.FindElements(By.Name("dataRow"));
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

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

            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[2]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleYes = driver.FindElement(By.XPath("/html/body/div/div/div[4]/div[1]/button"));
            IJavaScriptExecutor executorYes = (IJavaScriptExecutor)driver;
            executorYes.ExecuteScript("arguments[0].click();", eleYes);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            String Code = driver.FindElement(By.Id("nsx_code")).GetAttribute("value");
            //Check with value of DB
            CM_NSX_DTO finalRow = DataProvider.Instance.GetData<CM_NSX_DTO>("NhaSanXuat_Search", new { NSX_CODE = Code, RECORD_STATUS = 1 }).ToList().FirstOrDefault();

            //Assert
            Assert.AreEqual("1", finalRow.RECORD_STATUS);
            driver.Close();
        }
    }
}
