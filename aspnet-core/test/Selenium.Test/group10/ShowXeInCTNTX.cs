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
using Selenium.Test.group10.DTO;
using System.Collections.Generic;

namespace Selenium.Test.group10
{
    [TestClass]
    public class ShowXeInCTNTX : Load
    {
        public ShowXeInCTNTX()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        public void Show_History_Xe()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ntx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            Thread.Sleep(8000);

            //Act;
            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[4]")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Id("ntx_name")).GetAttribute("value") != "");

            //Check with value of DB
            //get id xe
            String ntxCode = driver.FindElement(By.Id("ntx_code")).GetAttribute("value");
            NguoiThueXe_DTO ntx = DataProvider.Instance.GetData<NguoiThueXe_DTO>("NguoiThueXe_Search", new { NTX_CODE = ntxCode , RECORD_STATUS = "1"}).ToList().FirstOrDefault();

            var list = DataProvider.Instance.GetData<Xe_DTO>("XE_ByNTX_ID", new { NTX_ID = ntx.NTX_ID }).ToList();
            var totalCount = list.Count();

            var text = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div[3]/div[2]/span")).Text;
            
            //Assert
            Assert.AreEqual(text, "Tổng cộng: " + totalCount);
            
            driver.Close();

        }
    }
}
