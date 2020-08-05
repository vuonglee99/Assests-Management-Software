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
    public class DeleteCTBD : Load
    {
        public DeleteCTBD()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";
        }

        [TestMethod]
        public void Delete_CTBD_With_OK_Status()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-duong-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            Thread.Sleep(8000);

            //Act;
            IWebElement ele0 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor0 = (IJavaScriptExecutor)driver;
            executor0.ExecuteScript("arguments[0].click();", ele0);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[3]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[2]/div/div/div[1]/div[1]/input")).GetAttribute("value") != "");

            IWebElement ele2 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[3]/div[2]/div/div/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor2 = (IJavaScriptExecutor)driver;
            executor2.ExecuteScript("arguments[0].click();", ele2);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[3]/ul/li[3]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            wait.Until(e => e.FindElement(By.Id("total_price")).GetAttribute("value") != "");

            String Code = driver.FindElement(By.Id("code")).GetAttribute("value");
            String name = driver.FindElement(By.Id("name")).GetAttribute("value");
            String Quantity = driver.FindElement(By.Id("quantity")).GetAttribute("value");
            String UnitPrice = driver.FindElement(By.Id("unit_price")).GetAttribute("value");
            string[] arrListStr = (driver.Url).Split('/');
            string id = arrListStr[arrListStr.Length - 1];

            var list = DataProvider.Instance.GetData<dynamic>("CTBaoDuong_SEARCHBYCODE", new ChiTietBaoDuong_DTO { CTBD_CODE = Code });

            IWebElement ele3 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[3]"));
            IJavaScriptExecutor executor3 = (IJavaScriptExecutor)driver;
            executor3.ExecuteScript("arguments[0].click();", ele3);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement ele4 = driver.FindElement(By.CssSelector("body > div.swal-overlay.swal-overlay--show-modal > div > div.swal-footer > div:nth-child(2) > button"));
            IJavaScriptExecutor executor4 = (IJavaScriptExecutor)driver;
            executor4.ExecuteScript("arguments[0].click();", ele4);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Check with value of DB
            ChiTietBaoDuong_DTO finalRow = DataProvider.Instance.GetData<ChiTietBaoDuong_DTO>("CTBaoDuong_GETBYID", new { CTBD_ID = id, RECORD_STATUS = '1' }).ToList().FirstOrDefault();
            
            //Assert
            if (list.Count() > 1 && finalRow.AUTH_STATUS == null)
            {
                Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--error")).Displayed);
            }
            else
            {
                Assert.AreEqual("1", finalRow.RECORD_STATUS);
            }
            
            driver.Close();

        }

        [TestMethod]
        public void Cancel_Delete_CTBD()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-duong-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            Thread.Sleep(8000);

            //Act;
            IWebElement ele0 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor0 = (IJavaScriptExecutor)driver;
            executor0.ExecuteScript("arguments[0].click();", ele0);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[3]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[2]/div/div/div[1]/div[1]/input")).GetAttribute("value") != "");

            IWebElement ele2 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[3]/div[2]/div/div/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[2]"));
            IJavaScriptExecutor executor2 = (IJavaScriptExecutor)driver;
            executor2.ExecuteScript("arguments[0].click();", ele2);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[3]/ul/li[3]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            wait.Until(e => e.FindElement(By.Id("total_price")).GetAttribute("value") != "");

            String Code = driver.FindElement(By.Id("code")).GetAttribute("value");
            String name = driver.FindElement(By.Id("name")).GetAttribute("value");
            String Quantity = driver.FindElement(By.Id("quantity")).GetAttribute("value");
            String UnitPrice = driver.FindElement(By.Id("unit_price")).GetAttribute("value");
            string[] arrListStr = (driver.Url).Split('/');
            string id = arrListStr[arrListStr.Length - 1];

            IWebElement ele3 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[3]"));
            IJavaScriptExecutor executor3 = (IJavaScriptExecutor)driver;
            executor3.ExecuteScript("arguments[0].click();", ele3);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            wait.Until(e => e.FindElement(By.ClassName("swal-icon--warning")).Displayed);

            IWebElement ele4 = driver.FindElement(By.CssSelector("body > div.swal-overlay.swal-overlay--show-modal > div > div.swal-footer > div:nth-child(1) > button"));
            IJavaScriptExecutor executor4 = (IJavaScriptExecutor)driver;
            executor4.ExecuteScript("arguments[0].click();", ele4);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Check with value of DB
            ChiTietBaoDuong_DTO finalRow = DataProvider.Instance.GetData<ChiTietBaoDuong_DTO>("CTBaoDuong_GETBYID", new { CTBD_ID = id, RECORD_STATUS = '1' }).ToList().FirstOrDefault();
            //Assert

            Assert.AreEqual(name, finalRow.CTBD_NAME);
            Assert.AreEqual(Code, finalRow.CTBD_CODE);
            Assert.AreEqual(Quantity, (finalRow.CTBD_QUANTITY).ToString());
            Assert.AreEqual(UnitPrice, (finalRow.CTBD_UNIT_PRICE).ToString());
            Assert.AreEqual("1", finalRow.RECORD_STATUS);
            Assert.AreNotEqual("PEN_DELETE", finalRow.APPROVE_STATUS);

            driver.Close();
        }

        [TestMethod]
        public void Delete_CTBD_With_No_Row_Clicked()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-duong-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            Thread.Sleep(8000);

            //Act;
            IWebElement ele0 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[1]"));
            IJavaScriptExecutor executor0 = (IJavaScriptExecutor)driver;
            executor0.ExecuteScript("arguments[0].click();", ele0);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[3]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[2]/div/div/div[1]/div[1]/input")).GetAttribute("value") != "");

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[3]/ul/li[4]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--error")).Displayed);
            driver.Close();
        }
    }
}
