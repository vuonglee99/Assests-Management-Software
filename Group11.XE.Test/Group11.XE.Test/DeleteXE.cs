using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Group11.XE.Test.Helper;
using System.Linq;
using Group11.XE.Test.XEDTO;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Threading;

namespace Group11.XE.Test
{
    [TestClass]
    public class DeleteXE : Load
    {
        public DeleteXE()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        public void Delete_XE_With_OK_Status()
        {
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
   

            String Code = ele.Text;


            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[2]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleYes = driver.FindElement(By.XPath("/html/body/div[1]/div/div[4]/div[2]/button"));
            IJavaScriptExecutor executorYes = (IJavaScriptExecutor)driver;
            executorYes.ExecuteScript("arguments[0].click();", eleYes);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            

            
            //Check with value of DB
            XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            //Assert.AreEqual("0", finalRow.RECORD_STATUS);
            Assert.IsNull(finalRow);

            driver.Close();

        }

        [TestMethod]
        public void Cancel_Delete_XE()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/xe-group11");
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
            XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            //Assert.AreEqual("0", finalRow.RECORD_STATUS);
            Assert.IsNotNull(finalRow);

            driver.Close();
        }

        [TestMethod]
        public void Delete_XE_CT_With_OK_Status()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/xe-group11");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[" + number + "]/td[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);



            IWebElement eleXem = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[3]"));
            IJavaScriptExecutor executorXem = (IJavaScriptExecutor)driver;
            executorXem.ExecuteScript("arguments[0].click();", eleXem);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            String Code = driver.FindElement(By.Name("xE_CODE")).GetAttribute("value");

            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[2]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleYes = driver.FindElement(By.XPath("/html/body/div/div/div[4]/div[2]/button"));
            IJavaScriptExecutor executorYes = (IJavaScriptExecutor)driver;
            executorYes.ExecuteScript("arguments[0].click();", eleYes);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);


            //Check with value of DB
            XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            //Assert.AreEqual("0", finalRow.RECORD_STATUS);
            Assert.IsNull(finalRow);

            driver.Close();

        }


        [TestMethod]
        public void Cancel_Delete_XE_CT()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/xe-group11");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[" + number + "]/td[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleXem = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[3]"));
            IJavaScriptExecutor executorXem = (IJavaScriptExecutor)driver;
            executorXem.ExecuteScript("arguments[0].click();", eleXem);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            String Code = driver.FindElement(By.Name("xE_CODE")).GetAttribute("value");

            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[2]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleYes = driver.FindElement(By.XPath("/html/body/div/div/div[4]/div[1]/button"));
            IJavaScriptExecutor executorYes = (IJavaScriptExecutor)driver;
            executorYes.ExecuteScript("arguments[0].click();", eleYes);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);




            //Check with value of DB
            XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            //Assert.AreEqual("0", finalRow.RECORD_STATUS);
            Assert.IsNotNull(finalRow);

            driver.Close();
        }
    }
}

