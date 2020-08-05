using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Test.Helper;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace Selenium.Test.group7
{
    [TestClass]
    public class TestLoaiXe : Load
    {

        public TestLoaiXe()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";
            
        }

        [TestMethod]
        [DataRow("00001", "xe")]
        public void SearchLoaiXe(String searchText, String searchName)
        {
            Login();

            System.Threading.Thread.Sleep(2000);

            driver.Navigate().GoToUrl(homeURL + "/app/admin/loai-xe");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Id("search-by-code")));
            Thread.Sleep(2000);

            //Act;
            driver.FindElement(By.Id("search-by-code")).SendKeys(searchText);
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.ClassName("fa-search")).Click();

            System.Threading.Thread.Sleep(5000);

            //Check with value of DB
            List<dynamic> listRow = DataProvider.Instance.GetData<dynamic>("LoaiXe_SearchWithOption", new { TO_SEARCH = searchText, SEARCH_TYPE = "LX_ID" }).ToList();
            var count = driver.FindElements(By.ClassName("total-records-count"))[0].Text;

            Assert.AreEqual(listRow.Count().ToString(), count);

            //Now check name
            wait.Until(e => e.FindElement(By.Id("search-by-name")));
            Thread.Sleep(2000);

            //Act;
            driver.FindElement(By.Id("search-by-code")).SendKeys("");
            driver.FindElement(By.Id("search-by-name")).SendKeys(searchName);
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.ClassName("fa-search")).Click();
            System.Threading.Thread.Sleep(5000);
            //Check with value of DB
            List<dynamic> listRow2 = DataProvider.Instance.GetData<dynamic>("LoaiXe_SearchWithOption", new { TO_SEARCH = searchName, SEARCH_TYPE = "LX_ID" }).ToList();
            var count2 = driver.FindElements(By.ClassName("total-records-count"))[0].Text;

            Assert.AreEqual(listRow.Count().ToString(), count);


            System.Threading.Thread.Sleep(5000);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //Assert

            driver.Close();

        }

        [TestMethod]
        [DataRow(0)]
        public void DeleteLoaiXe(int pos)
        {
            Login();

            System.Threading.Thread.Sleep(2000);

            driver.Navigate().GoToUrl(homeURL + "/app/admin/loai-xe");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            wait.Until(e => e.FindElements(By.ClassName("ng-tns-c2-0")));
            Thread.Sleep(1000);

            //Act;
            driver.FindElements(By.ClassName("ng-tns-c2-0"))[pos].Click();
            Thread.Sleep(1000);
            driver.FindElement(By.ClassName("fa-trash")).Click();
            Thread.Sleep(1000);
            var  title = driver.FindElement(By.ClassName("swal-title"));

            Assert.AreEqual(title.Text, "Bạn có chắc muốn xóa những dữ liệu đã chọn không?");


            System.Threading.Thread.Sleep(5000);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //Assert

            driver.Close();
        }
    }
}
