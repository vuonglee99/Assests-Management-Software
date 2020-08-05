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

namespace Selenium.Test.group10
{
    [TestClass]
    public class CreateCTBD : Load
    {
        public CreateCTBD()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        [DataRow("", "1111", "1", "100000")]
        [DataRow("Thay máy làm mát", "", "1", "100000")]
        [DataRow("Thay máy làm mát", "1111", "", "100000")]
        [DataRow("Thay máy làm mát", "1111", "1", "")]
        public void Create_CTBD_With_Empty(String Name, String Code, String Quantity, String UnitPrice)
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

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[3]/ul/li[1]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //wait.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[2]/div/div/div[1]/div[1]/input")).GetAttribute("value") == "");
            //Act

            driver.FindElement(By.Id("name")).SendKeys(Name);
            driver.FindElement(By.Id("code")).SendKeys(Code);
            driver.FindElement(By.Id("quantity")).SendKeys(Quantity);
            driver.FindElement(By.Id("unit_price")).SendKeys(UnitPrice);


            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.Name("empty_field")).Displayed);
            driver.Close();
        }

        [TestMethod]
        [DataRow("Thay máy làm máttttttttttttttttttttttttttttttttttttttttttttttt", "1111", "1", "100000")]
        [DataRow("Thay máy làm mát", "1111111111111111111111111111111111111", "1", "100000")]
        public void Create_CTBD_With_MaxSize(String Name, String Code, String Quantity, String UnitPrice)
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

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[3]/ul/li[1]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //wait.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[2]/div/div/div[1]/div[1]/input")).GetAttribute("value") == "");
            //Act

            driver.FindElement(By.Id("name")).SendKeys(Name);
            driver.FindElement(By.Id("code")).SendKeys(Code);
            driver.FindElement(By.Id("quantity")).SendKeys(Quantity);
            driver.FindElement(By.Id("unit_price")).SendKeys(UnitPrice);


            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.IsTrue(driver.FindElement(By.Name("maxsize_field")).Displayed);
            driver.Close();
        }

        [TestMethod]
        [DataRow("Thay máy làm mát", "1111", "1abc*", "100000")]
        [DataRow("Thay máy làm mát", "1111", "1", "100000abc*")]
        public void Create_CTBD_With_CheckNumber(String Name, String Code, String Quantity, String UnitPrice)
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

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[3]/ul/li[1]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //wait.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[2]/div/div/div[1]/div[1]/input")).GetAttribute("value") == "");
            //Act

            driver.FindElement(By.Id("name")).SendKeys(Name);
            driver.FindElement(By.Id("code")).SendKeys(Code);
            driver.FindElement(By.Id("quantity")).SendKeys(Quantity);
            driver.FindElement(By.Id("unit_price")).SendKeys(UnitPrice);


            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            if(Quantity == "1abc*")
            {
                Assert.IsTrue(driver.FindElement(By.Name("quantity_field")).Displayed);
            }    
            if(UnitPrice == "100000abc*")
            {
                Assert.IsTrue(driver.FindElement(By.Name("unit_price_field")).Displayed);
            }
            driver.Close();
        }

        [TestMethod]
        [DataRow("Thay máy làm mát", "12345", "1", "100000")]
        public void Create_CTBD_With_DuplicateCode(String Name, String Code, String Quantity, String UnitPrice)
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

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[3]/ul/li[1]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //wait.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[2]/div/div/div[1]/div[1]/input")).GetAttribute("value") == "");
            //Act
            var url = driver.Url;
            driver.FindElement(By.Id("name")).SendKeys(Name);
            driver.FindElement(By.Id("code")).SendKeys(Code);
            driver.FindElement(By.Id("quantity")).SendKeys(Quantity);
            driver.FindElement(By.Id("unit_price")).SendKeys(UnitPrice);


            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //Assert
            Assert.AreNotEqual(url, driver.Url);
            driver.Close();
        }

        [TestMethod]
        [DataRow("Thay máy làm mát", "1", "100000")]
        public void Create_CTBD_With_Success_Wait_Approve(String Name, String Quantity, String UnitPrice)
        {
            String Code = (new Random().Next(1, 99999999)).ToString();
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

            IWebElement ele1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[3]/ul/li[1]"));
            IJavaScriptExecutor executor1 = (IJavaScriptExecutor)driver;
            executor1.ExecuteScript("arguments[0].click();", ele1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //wait.Until(e => e.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/div/div[2]/div/div/div[1]/div[1]/input")).GetAttribute("value") == "");
            //Act
            var url = driver.Url;
            driver.FindElement(By.Id("name")).SendKeys(Name);
            driver.FindElement(By.Id("code")).SendKeys(Code);
            driver.FindElement(By.Id("quantity")).SendKeys(Quantity);
            driver.FindElement(By.Id("unit_price")).SendKeys(UnitPrice);


            driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            ChiTietBaoDuong_DTO row = DataProvider.Instance.GetData<ChiTietBaoDuong_DTO>("CTBaoDuong_SEARCHBYCODE", new { CTBD_CODE = Code }).ToList().FirstOrDefault();
            //Assert
            Assert.AreEqual(Name, row.CTBD_NAME);
            Assert.AreEqual(Code, row.CTBD_CODE);
            Assert.AreEqual(Quantity, (row.CTBD_QUANTITY).ToString());
            Assert.AreEqual(UnitPrice, (row.CTBD_UNIT_PRICE).ToString());
            Assert.AreEqual(null, row.AUTH_STATUS);
            Assert.AreEqual("1", (row.RECORD_STATUS).ToString());
            Assert.AreEqual("PEN_INSERT", row.APPROVE_STATUS);
            driver.Close();
        }
    }
}
