using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Test.Helper;
using System.Linq;
using Selenium.Test.DVT_DTO;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Data;

namespace Selenium.Test.group6
{
    [TestClass]
    public class TestDVT : Load
    {
        public TestDVT()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }


        [TestMethod]
        [DataRow("003")]
      //  [DataRow("002")]
        //[DataRow("005")]
        public void Search_By_Code(String Code)
        {
            Login();
            System.Threading.Thread.Sleep(3000);


            driver.Navigate().GoToUrl(homeURL + "/app/admin/donvitinh");

            System.Threading.Thread.Sleep(2000);


            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div/div[1]/input")).SendKeys(Code);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
         

            IWebElement result = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[1]/div/div/button[1]"));

            System.Threading.Thread.Sleep(2000);
            result.Click();
            System.Threading.Thread.Sleep(5000);
            List<dynamic> listRow = DataProvider.Instance.GetData<dynamic>("DVT_searchFilter", new { PageNumber = "1", PageSize = "10", DVT_CODE = Code,DVT_NAME="" }).ToList();


            Assert.AreEqual(listRow.Count().ToString(), driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[3]/div/div/div/span[2]")).GetAttribute("innerHTML"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            System.Threading.Thread.Sleep(2000);
            driver.Close();

        }
        [TestMethod]
        [DataRow("co")]
        //[DataRow("asdsadsadsadsa")]
        //[DataRow("M")]
        public void Search_By_Name(String Name)
        {
            Login();
            System.Threading.Thread.Sleep(3000);


            driver.Navigate().GoToUrl(homeURL + "/app/admin/donvitinh");

            System.Threading.Thread.Sleep(2000);


            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div/div[2]/input")).SendKeys(Name);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


            IWebElement result = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[1]/div/div/button[1]"));

            System.Threading.Thread.Sleep(2000);
            result.Click();
            System.Threading.Thread.Sleep(5000);
            List<dynamic> listRow = DataProvider.Instance.GetData<dynamic>("DVT_searchFilter", new { PageNumber = "1", PageSize = "10", DVT_CODE = "", DVT_NAME = Name }).ToList();


            Assert.AreEqual(listRow.Count().ToString(), driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[3]/div/div/div/span[2]")).GetAttribute("innerHTML"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            System.Threading.Thread.Sleep(2000);
            driver.Close();


        }
        [TestMethod]

        [DataRow("001", "co")]
        //[DataRow("", "n")]
        //[DataRow("00", "m")]
        //[DataRow("00", "")]
        public void Search_By_NameAndCode(String Code, String Name)
        {
            Login();
            System.Threading.Thread.Sleep(3000);


            driver.Navigate().GoToUrl(homeURL + "/app/admin/donvitinh");

            System.Threading.Thread.Sleep(2000);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div/div[1]/input")).SendKeys(Code);
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[2]/div/div/div/div[2]/input")).SendKeys(Name);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


            IWebElement result = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[1]/div/div/button[1]"));

            System.Threading.Thread.Sleep(2000);
            result.Click();
            System.Threading.Thread.Sleep(5000);
            List<dynamic> listRow = DataProvider.Instance.GetData<dynamic>("DVT_searchFilter", new { PageNumber = "1", PageSize = "10", DVT_CODE = Code, DVT_NAME = Name }).ToList();


            Assert.AreEqual(listRow.Count().ToString(), driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[3]/div/div/div/span[2]")).GetAttribute("innerHTML"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            System.Threading.Thread.Sleep(2000);
            driver.Close();
        }

        [TestMethod]
        public void DeleteDVT()
        {
            Login();


            System.Threading.Thread.Sleep(5000);
            driver.Navigate().GoToUrl(homeURL + "/app/admin/donvitinh");
            //driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[1]/side-bar-menu/nav/div[1]/div/ul/li[4]/nav/ul/li[4]/a/span")).Click();
            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait2.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[3]/div/p-table/div/div/table/tbody/tr[1]/td[2]")));
            System.Threading.Thread.Sleep(5000);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[3]/div/p-table/div/div/table/tbody/tr[1]/td[2]")).Click();
            string name = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[3]/div/p-table/div/div/table/tbody/tr[1]/td[3]")).GetAttribute("innerHTML").ToString();
            String Code = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[3]/div/p-table/div/div/table/tbody/tr[1]/td[2]")).GetAttribute("innerHTML").ToString();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[1]/div/div/button[3]")).Click();

            System.Threading.Thread.Sleep(2000);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div/div[4]/div/div/div/div[3]/button[2]")).Click();

            // driver.FindElement(By.XPath("*[@id='exampleModal']/div/div/div[3]/button[2]"));


            dynamic result = DataProvider.Instance.GetData<dynamic>("DVT_searchFilter", new { PageNumber = "1", PageSize = "10", DVT_CODE = Code, DVT_NAME = name }).ToList().FirstOrDefault();



            Assert.AreEqual(null, result);
            System.Threading.Thread.Sleep(2000);
            driver.Close();




        }
    }
}
