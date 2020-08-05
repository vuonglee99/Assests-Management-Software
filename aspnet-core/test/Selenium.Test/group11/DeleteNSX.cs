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
    public class DeleteNSX : Load
    {
        public DeleteNSX()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        public void Delete_NSX_With_OK_Status()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/nsx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr["+number+"]/td[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            String Code = ele.Text;


            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[2]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleYes = driver.FindElement(By.XPath("/html/body/div/div/div[4]/div[2]/button"));
            IJavaScriptExecutor executorYes = (IJavaScriptExecutor)driver;
            executorYes.ExecuteScript("arguments[0].click();", eleYes);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);




            //Check with value of DB
            NSX_DTO finalRow = DataProvider.Instance.GetData<NSX_DTO>("NhaSanXuat_SEARCH", new { NSX_CODE = Code, RECORD_STATUS = 1 }).ToList().FirstOrDefault();

            //Assert
            //Assert.AreEqual("0", finalRow.RECORD_STATUS);
            Assert.IsNull(finalRow);

            driver.Close();

        }

        [TestMethod]
        public void Delete_NSX_With_Cancel()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/nsx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[" + number + "]/td[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            String Code = ele.Text;


            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[2]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleYes = driver.FindElement(By.XPath("/html/body/div/div/div[4]/div[1]/button"));
            IJavaScriptExecutor executorYes = (IJavaScriptExecutor)driver;
            executorYes.ExecuteScript("arguments[0].click();", eleYes);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);




            //Check with value of DB
            NSX_DTO finalRow = DataProvider.Instance.GetData<NSX_DTO>("NhaSanXuat_SEARCH", new { NSX_CODE = Code, RECORD_STATUS = 1 }).ToList().FirstOrDefault();

            //Assert
            //Assert.AreEqual("0", finalRow.RECORD_STATUS);
            Assert.AreEqual("1",finalRow.RECORD_STATUS);

            driver.Close();

        }
    }
}