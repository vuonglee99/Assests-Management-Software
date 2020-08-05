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
    public class UpdateCTBD : Load
    {
        public UpdateCTBD()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        [DataRow("", "1", "100000")]
        [DataRow("Thay máy làm mát 1", "", "100000")]
        [DataRow("Thay máy làm mát 1", "1", "")]
        public void Update_CTBD_With_Empty(String Name, String Quantity, String UnitPrice)
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

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[3]/ul/li[2]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //wait.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[2]/div/div/div[1]/div[1]/input")).GetAttribute("value") == "");
            //Act
            wait.Until(e => e.FindElement(By.Id("total_price")).GetAttribute("value") != "");

            String Code = driver.FindElement(By.Id("code")).GetAttribute("value");
            driver.FindElement(By.Id("name")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("name")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("name")).SendKeys(Name);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("quantity")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("quantity")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("quantity")).SendKeys(Quantity);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("unit_price")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("unit_price")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("unit_price")).SendKeys(UnitPrice);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[2]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.Name("empty_field")).Displayed);
            driver.Close();
        }

        [TestMethod]
        [DataRow("Thay máy làm máttttttttttttttttttttttttttttttttttttttttttttttt")]
        public void Update_CTBD_With_MaxSize(String Name)
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

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[3]/ul/li[2]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //wait.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[2]/div/div/div[1]/div[1]/input")).GetAttribute("value") == "");
            //Act
            wait.Until(e => e.FindElement(By.Id("total_price")).GetAttribute("value") != "");

            String Code = driver.FindElement(By.Id("code")).GetAttribute("value");
            driver.FindElement(By.Id("name")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("name")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("name")).SendKeys(Name);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[2]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.Name("maxsize_field")).Displayed);
            driver.Close();
        }

        [TestMethod]
        [DataRow("1abc*", "100000")]
        [DataRow("1", "100000abc*")]
        public void Update_CTBD_With_CheckNumber(String Quantity, String UnitPrice)
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

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[3]/ul/li[2]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //wait.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[2]/div/div/div[1]/div[1]/input")).GetAttribute("value") == "");
            //Act
            wait.Until(e => e.FindElement(By.Id("total_price")).GetAttribute("value") != "");

            driver.FindElement(By.Id("quantity")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("quantity")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("quantity")).SendKeys(Quantity);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Id("unit_price")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("unit_price")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("unit_price")).SendKeys(UnitPrice);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[2]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            if (Quantity == "1abc*")
            {
                Assert.IsTrue(driver.FindElement(By.Name("quantity_field")).Displayed);
            }
            if (UnitPrice == "100000abc*")
            {
                Assert.IsTrue(driver.FindElement(By.Name("unit_price_field")).Displayed);
            }
            driver.Close();
        }

        [TestMethod]
        [DataRow("Thay máy làm mát 2")]
        public void Update_CTBD_With_Success_Wait_Approve(String Name)
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

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[3]/ul/li[2]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //wait.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[2]/div/div/div[1]/div[1]/input")).GetAttribute("value") == "");
            //Act
            wait.Until(e => e.FindElement(By.Id("total_price")).GetAttribute("value") != "");
            String Code = driver.FindElement(By.Id("code")).GetAttribute("value");
            String name = driver.FindElement(By.Id("name")).GetAttribute("value");
            String Quantity = driver.FindElement(By.Id("quantity")).GetAttribute("value");
            String UnitPrice = driver.FindElement(By.Id("unit_price")).GetAttribute("value");
            string[] arrListStr = (driver.Url).Split('/');
            string id = arrListStr[arrListStr.Length - 1];
            driver.FindElement(By.Id("name")).SendKeys(Keys.Control + "a");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("name")).SendKeys("\u0008");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            driver.FindElement(By.Id("name")).SendKeys(Name);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            var list = DataProvider.Instance.GetData<dynamic>("CTBaoDuong_SEARCHBYCODE", new ChiTietBaoDuong_DTO { CTBD_CODE = Code });

            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[2]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            ChiTietBaoDuong_DTO row = DataProvider.Instance.GetData<ChiTietBaoDuong_DTO>("CTBaoDuong_GETBYID", new { CTBD_ID = id, RECORD_STATUS = '1' }).ToList().FirstOrDefault();
            //Assert
            if (list.Count() > 1)
            {
                Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--error")).Displayed);
            }
            else
            {
                Assert.AreEqual(name, row.CTBD_NAME);
                Assert.AreEqual(Code, row.CTBD_CODE);
                Assert.AreEqual(Quantity, (row.CTBD_QUANTITY).ToString());
                Assert.AreEqual(UnitPrice, (row.CTBD_UNIT_PRICE).ToString());
                Assert.AreEqual("1", (row.RECORD_STATUS).ToString());
            }
            //Assert
            driver.Close();
        }

        [TestMethod]
        public void Update_CTBD_With_No_Row_Clicked()
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

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[3]/ul/li[2]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--error")).Displayed);
            driver.Close();
        }
    }
}
