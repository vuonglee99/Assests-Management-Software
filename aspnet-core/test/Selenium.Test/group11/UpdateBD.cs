using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using Selenium.Test.group11.DTO;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using Selenium.Test.Helper;

namespace Selenium.Test.group11
{
    [TestClass]
    public class UpdateBD : Load
    {
        public UpdateBD()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        [DataRow("Hung","Quan 5,HCM")]
        [DataRow("TienPhat", "Quan 6,HCM")]
        public void Update_BD_With_OK_Status(String Garage,String Address)
        {
            
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-duong-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act

            //Select random BD
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr["+number+"]/td[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            IWebElement eleEdit = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[2]"));
            IJavaScriptExecutor executorEdit = (IJavaScriptExecutor)driver;
            executorEdit.ExecuteScript("arguments[0].click();", eleEdit);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            //Act;
            driver.FindElement(By.Name("bD_GARAGE")).Clear();
            driver.FindElement(By.Name("bD_GARAGE")).SendKeys(Garage);
            driver.FindElement(By.Name("bD_ADDRESS")).Clear();
            driver.FindElement(By.Name("bD_ADDRESS")).SendKeys(Address);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //String Code = driver.FindElement(By.Name("xE_CODE")).GetAttribute("value");


            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnSaveNTX")));
            IWebElement eleUpdate = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[2]"));
            IJavaScriptExecutor executorUpdate = (IJavaScriptExecutor)driver;
            executorUpdate.ExecuteScript("arguments[0].click();", eleUpdate);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);


            //Check with value of DB
            //XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--success")).Displayed);

            driver.Close();
        }

        [TestMethod]

        [DataRow("Garage TienPhat")]
        public void Update_BD_With_Empty(String Garage)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-duong-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act

            //Select random BD
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[" + number + "]/td[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            IWebElement eleEdit = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[2]"));
            IJavaScriptExecutor executorEdit = (IJavaScriptExecutor)driver;
            executorEdit.ExecuteScript("arguments[0].click();", eleEdit);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            //Act;

            driver.FindElement(By.Name("bD_GARAGE")).Clear();
            driver.FindElement(By.Name("bD_GARAGE")).SendKeys(Garage);
            driver.FindElement(By.Name("bD_ADDRESS")).Clear();
            driver.FindElement(By.Name("bD_TO_DT")).Clear();
            driver.FindElement(By.Name("bD_FROM_DT")).Clear();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //String Code = driver.FindElement(By.Name("xE_CODE")).GetAttribute("value");


            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnSaveNTX")));
            IWebElement eleUpdate = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[2]"));
            IJavaScriptExecutor executorUpdate = (IJavaScriptExecutor)driver;
            executorUpdate.ExecuteScript("arguments[0].click();", eleUpdate);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);


            //Check with value of DB
            //XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--success")).Displayed);

            driver.Close();
        }

        [TestMethod]

        [DataRow("Tien Phattttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt", "Quan5")]
        [DataRow("Hung", "Quan 55555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555")]
        public void Update_BD_With_MaxSize(String Garage, String Address)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-duong-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act

            //Select random BD
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[" + number + "]/td[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            IWebElement eleEdit = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[2]"));
            IJavaScriptExecutor executorEdit = (IJavaScriptExecutor)driver;
            executorEdit.ExecuteScript("arguments[0].click();", eleEdit);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            //Act;

            driver.FindElement(By.Name("bD_GARAGE")).Clear();
            driver.FindElement(By.Name("bD_GARAGE")).SendKeys(Garage);
            driver.FindElement(By.Name("bD_ADDRESS")).Clear();
            driver.FindElement(By.Name("bD_ADDRESS")).SendKeys(Address);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            //String Code = driver.FindElement(By.Name("xE_CODE")).GetAttribute("value");


            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnSaveNTX")));
            IWebElement eleUpdate = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[2]"));
            IJavaScriptExecutor executorUpdate = (IJavaScriptExecutor)driver;
            executorUpdate.ExecuteScript("arguments[0].click();", eleUpdate);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);


            //Check with value of DB
            //XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--error")).Displayed);

            driver.Close();
        }
    }
}