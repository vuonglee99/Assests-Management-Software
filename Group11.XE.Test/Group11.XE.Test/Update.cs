using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using Group11.XE.Test.XEDTO;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using Group11.XE.Test.Helper;

namespace Group11.XE.Test
{
    [TestClass]
    public class UpdateXE : Load
    {
        public UpdateXE()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        public void Update_XE_With_OK_Status()
        {
            String Name = new Random().Next().ToString();
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/xe-group11");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr["+number+"]/td[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            IWebElement eleEdit = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[3]"));
            IJavaScriptExecutor executorEdit = (IJavaScriptExecutor)driver;
            executorEdit.ExecuteScript("arguments[0].click();", eleEdit);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            //Act;
            driver.FindElement(By.Name("xE_NAME")).Clear();
            driver.FindElement(By.Name("xE_NAME")).SendKeys(Name);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            String Code = driver.FindElement(By.Name("xE_CODE")).GetAttribute("value");


            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnSaveNTX")));
            IWebElement eleUpdate = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[1]/i"));
            IJavaScriptExecutor executorUpdate = (IJavaScriptExecutor)driver;
            executorUpdate.ExecuteScript("arguments[0].click();", eleUpdate);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            
            //Check with value of DB
            XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            Assert.AreEqual(Name, finalRow.XE_NAME);

            driver.Close();
        }

        [TestMethod]
        public void Update_NSX_With_Empty()
        {
            String Name = "";
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/xe-group11");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[2]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr["+number+"]/td[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            IWebElement eleEdit = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[3]"));
            IJavaScriptExecutor executorEdit = (IJavaScriptExecutor)driver;
            executorEdit.ExecuteScript("arguments[0].click();", eleEdit);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            //Act;

            driver.FindElement(By.Name("xE_NAME")).Clear();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            String Code = driver.FindElement(By.Name("xE_CODE")).GetAttribute("value");


            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnSaveNTX")));
            IWebElement eleUpdate = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[1]/i"));
            IJavaScriptExecutor executorUpdate = (IJavaScriptExecutor)driver;
            executorUpdate.ExecuteScript("arguments[0].click();", eleUpdate);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);


            //Check with value of DB
            XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            Assert.AreNotEqual(Name, finalRow.XE_NAME);

            driver.Close();
        }

        [TestMethod]
        [DataRow("Leadeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee", "Vang", "77H7-77777")]
        //[DataRow("Lead", "Vanggggggggggggggggggggg", "77H7-77777")]
        //[DataRow("Lead", "Vang", "77H7-7777777777777777777")]
        public void Update_NSX_With_MaxSize(String Name, String Color,String License_Plate)
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/xe-group11");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[2]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[" + number + "]/td[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            IWebElement eleEdit = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[3]"));
            IJavaScriptExecutor executorEdit = (IJavaScriptExecutor)driver;
            executorEdit.ExecuteScript("arguments[0].click();", eleEdit);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            //Act;

            driver.FindElement(By.Name("xE_NAME")).Clear();
            driver.FindElement(By.Name("xE_NAME")).SendKeys(Name);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Name("xE_COLOR")).Clear();
            driver.FindElement(By.Name("xE_COLOR")).SendKeys(Color);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            driver.FindElement(By.Name("xE_LICENSE_PLATE")).Clear();
            driver.FindElement(By.Name("xE_LICENSE_PLATE")).SendKeys(License_Plate);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            String Code = driver.FindElement(By.Name("xE_CODE")).GetAttribute("value");


            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnSaveNTX")));
            IWebElement eleUpdate = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[1]/i"));
            IJavaScriptExecutor executorUpdate = (IJavaScriptExecutor)driver;
            executorUpdate.ExecuteScript("arguments[0].click();", eleUpdate);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);


            //Check with value of DB
            XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            Assert.AreNotEqual(Name, finalRow.XE_NAME);
            

            driver.Close();

        }
    }
}

