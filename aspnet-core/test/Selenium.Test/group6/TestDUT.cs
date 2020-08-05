using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Test.Helper;
using System.Linq;
using Selenium.Test.DUT_DTO;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Data;



namespace Selenium.Test.group6
{
    [TestClass]
     public class TestDUT : Load
    {
        public TestDUT()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }


        [TestMethod]
        [DataRow("a")]
        //[DataRow("")]
        //[DataRow("assssddddd")]
        public void Search_By_NameDUT(String Name)
        {
            Login();

            System.Threading.Thread.Sleep(3000);

            driver.Navigate().GoToUrl(homeURL + "/app/admin/douutien");
            System.Threading.Thread.Sleep(2000);

            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait2.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[2]/div/div[1]/div/div[2]/div/div/input")));


            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[2]/div/div[1]/div/div[2]/div/div/input")).SendKeys(Name);
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[2]/div/div[1]/div/div[1]/button")).Click();

            System.Threading.Thread.Sleep(5000);

            List<dynamic> listRow = DataProvider.Instance.GetData<dynamic>("DUT_SearchFilter", new { PageNumber = "1", PageSize = "10", DUT_NAME = Name, RECORD_STATUS = "1", SORT = "" }).ToList();

            string count = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[2]/div/div[3]/div/div/div/div/span")).GetAttribute("innerHTML").ToString();

            Assert.AreEqual(listRow.Count().ToString(), count.Substring(count.Length - 2)[0].ToString());
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            System.Threading.Thread.Sleep(2000);
            driver.Close();

        }
        [TestMethod]
        //[DataRow("a")]
        [DataRow("")]
        //[DataRow("assssddddd")]
        public void Search_By_NameDUT2(String Name)
        {
            Login();

            System.Threading.Thread.Sleep(3000);

            driver.Navigate().GoToUrl(homeURL + "/app/admin/douutien");
            System.Threading.Thread.Sleep(2000);

            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait2.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[2]/div/div[1]/div/div[2]/div/div/input")));


            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[2]/div/div[1]/div/div[2]/div/div/input")).SendKeys(Name);
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[2]/div/div[1]/div/div[1]/button")).Click();

            System.Threading.Thread.Sleep(5000);

            List<dynamic> listRow = DataProvider.Instance.GetData<dynamic>("DUT_SearchFilter", new { PageNumber = "1", PageSize = "10", DUT_NAME = Name, RECORD_STATUS = "1", SORT = "" }).ToList();

            string count = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[2]/div/div[3]/div/div/div/div/span")).GetAttribute("innerHTML").ToString();

            Assert.AreEqual(listRow.Count().ToString(), count.Substring(count.Length - 2)[0].ToString());
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            System.Threading.Thread.Sleep(2000);
            driver.Close();
        }
        [TestMethod]
        //[DataRow("a")]
        //[DataRow("")]
        [DataRow("assssddddd")]
        public void Search_By_NameDUT3(String Name)
        {
            Login();

            System.Threading.Thread.Sleep(3000);

            driver.Navigate().GoToUrl(homeURL + "/app/admin/douutien");
            System.Threading.Thread.Sleep(2000);

            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait2.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[2]/div/div[1]/div/div[2]/div/div/input")));


            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[2]/div/div[1]/div/div[2]/div/div/input")).SendKeys(Name);
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[2]/div/div[1]/div/div[1]/button")).Click();

            System.Threading.Thread.Sleep(5000);

            List<dynamic> listRow = DataProvider.Instance.GetData<dynamic>("DUT_SearchFilter", new { PageNumber = "1", PageSize = "10", DUT_NAME = Name , RECORD_STATUS="1",SORT ="" }).ToList();

            string count = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[2]/div/div[3]/div/div/div/div/span")).GetAttribute("innerHTML").ToString();

            Assert.AreEqual(listRow.Count().ToString(), count.Substring(count.Length - 2)[0].ToString());
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            System.Threading.Thread.Sleep(2000);
            driver.Close();
        }
        [TestMethod]
        public void Delete_DUT()
        {
            Login();
            System.Threading.Thread.Sleep(3000);


            driver.Navigate().GoToUrl(homeURL + "/app/admin/douutien");
            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait2.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[2]/div/div[3]/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]/td[2]")));


            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[2]/div/div[3]/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]/td[2]")).Click();
            string name = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[2]/div/div[3]/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]/td[3]")).GetAttribute("innerHTML").ToString();

            System.Threading.Thread.Sleep(2000);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[1]/div/div/div/div/button[2]")).Click();

            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[2]/div[1]/div/div/div[3]/button[2]")).Click();




            System.Threading.Thread.Sleep(7000);














            dynamic result = DataProvider.Instance.GetData<dynamic>("DUT_SearchFilter", new { PageNumber = "1", PageSize = "10", DUT_NAME = name, RECORD_STATUS="1",SORT = "" }).ToList().FirstOrDefault();

            Assert.AreEqual(result, null);
            System.Threading.Thread.Sleep(2000);
            driver.Close();



        }

        [TestMethod]
        [DataRow("aaaaaaaaaaaaaKhang", "aaaaaaaaaaaaaaaaaaaKhang")]
        public void Create_DUT(String Name, String Desc)
        {
            Login();
            System.Threading.Thread.Sleep(3000);


            driver.Navigate().GoToUrl(homeURL + "/app/admin/douutien");
            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait2.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[1]/div/div/div/div/button[1]")));
            System.Threading.Thread.Sleep(5000);
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/div[1]/div/div/div/div/button[1]")).Click();

            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[2]/div[2]/div/div/form/div[2]/div[2]/div/input")).SendKeys(Name);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[2]/div[2]/div/div/form/div[2]/div[3]/div/textarea")).SendKeys(Desc);

            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[2]/div[2]/div/div/form/div[3]/button[2]")).Click();


            System.Threading.Thread.Sleep(5000);

            dynamic result = DataProvider.Instance.GetData<dynamic>("DUT_SearchFilter", new { PageNumber = "1", PageSize = "10", DUT_NAME = Name, RECORD_STATUS = "1", SORT = "" }).ToList().FirstOrDefault();

            Assert.AreNotEqual(result, null);
            System.Threading.Thread.Sleep(2000);
            driver.Close();
        }
    }
}
