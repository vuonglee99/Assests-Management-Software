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
using Selenium.Test.group10.DTO;

namespace Selenium.Test.group10
{
    [TestClass]
    public class XoaPTX : Load
    {
        public XoaPTX()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        public void Delete_PTX_Not_Data()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ptx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            IWebElement btnDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", btnDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            Boolean b = (Boolean)(driver.FindElement(By.ClassName("swal-icon--error")).Displayed);

            Assert.IsTrue(b);
            driver.Close();
        }

       
        [TestMethod]
        public void Cancel_PTX()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ptx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            var list = DataProvider.Instance.GetData<PhieuThueXeDTO>("PhieuThueXe_Search", new {NTX_CODE = ""}).ToList();
            var totalCount = list.Count();

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement btnDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", btnDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement btnCancel = driver.FindElement(By.CssSelector("body > div > div > div.swal-footer > div:nth-child(1) > button"));
            IJavaScriptExecutor executorCancel = (IJavaScriptExecutor)driver;
            executorCancel.ExecuteScript("arguments[0].click();", btnCancel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Boolean b = (Boolean)(driver.FindElement(By.ClassName("swal-icon--success")).Displayed);
            var newlist = DataProvider.Instance.GetData<PhieuThueXeDTO>("PhieuThueXe_Search", new { NTX_CODE = "" }).ToList();
            var newtotalCount = newlist.Count();

            Assert.AreEqual(totalCount, newtotalCount);
            driver.Close();

        }

        [TestMethod]
        public void Delete_PTX()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ptx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement btnDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", btnDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement btnCancel = driver.FindElement(By.CssSelector("body > div > div > div.swal-footer > div:nth-child(2) > button"));
            IJavaScriptExecutor executorCancel = (IJavaScriptExecutor)driver;
            executorCancel.ExecuteScript("arguments[0].click();", btnCancel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            Boolean b = (Boolean)(driver.FindElement(By.ClassName("swal-icon--success__ring")).Displayed);

            Assert.IsTrue(b);
            driver.Close();

        }


    }
}
