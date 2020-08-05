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
    public class ShowNTXInCTX : Load
    {
        public ShowNTXInCTX()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        public void Show_History_NTX()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/xe-group11");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            Thread.Sleep(8000);

            //Act;
            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[3]")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Name("xE_CODE")).GetAttribute("value") != "");

            //Check with value of DB
            //get id xe
            String xeCode = driver.FindElement(By.Name("xE_CODE")).GetAttribute("value");
            List<Xe_DTO> xe = DataProvider.Instance.GetData<Xe_DTO>("XE_GETBYCODE", new Xe_DTO
            {
                XE_CODE = xeCode
            });
            var list = DataProvider.Instance.GetData<NguoiThueXe_DTO>("NguoiThueXe_ByXeID", new { XE_ID = xe.ElementAt(0).XE_ID }).ToList();
            var totalCount = list.Count();
            NguoiThueXe_DTO currentNTX = DataProvider.Instance.GetData<NguoiThueXe_DTO>("NguoiThueXe_ByXeId_HienTai", new { XE_ID = xe.ElementAt(0).XE_ID }).ToList().FirstOrDefault();

            var text = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[5]/div[3]/div/div/div/span")).Text;
            var currentNTX_name = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[5]/div[2]/div/div/div[1]/input")).GetAttribute("value");
            var currentNTX_code = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[5]/div[2]/div/div/div[2]/input")).GetAttribute("value");

            //Assert
            Assert.AreEqual(text, "Tổng cộng: " + totalCount);
            if(currentNTX == null)
            {
                Assert.AreEqual(currentNTX_name, "Trống");
                Assert.AreEqual(currentNTX_code, "Trống");
            }
            else
            {
                Assert.AreEqual(currentNTX_name, currentNTX.NTX_NAME);
                Assert.AreEqual(currentNTX_code, currentNTX.NTX_CODE);
            }   

            driver.Close();

        }
    }
}
