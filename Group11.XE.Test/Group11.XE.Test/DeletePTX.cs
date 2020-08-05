using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using Group11.XE.Test.PTXDTO;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using Group11.XE.Test.Helper;

namespace Group11.XE.Test
{
    [TestClass]
    public class DeletePTX : Load
    {
        public DeletePTX()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        public void Delete_PTX_With_OK_Status()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ptx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr["+number+"]/td[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            //String Code = ele.Text;


            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleYes = driver.FindElement(By.XPath("/html/body/div/div/div[4]/div[2]/button"));
            IJavaScriptExecutor executorYes = (IJavaScriptExecutor)driver;
            executorYes.ExecuteScript("arguments[0].click();", eleYes);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);




            //Check with value of DB
            //XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            //Assert.AreEqual("0", finalRow.RECORD_STATUS);
            Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--success")).Displayed);

            driver.Close();

        }

        [TestMethod]
        public void Cancel_Delete_PTX()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ptx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[" + number + "]/td[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            //String Code = ele.Text;


            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[1]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleYes = driver.FindElement(By.XPath("/html/body/div/div/div[4]/div[1]/button"));
            IJavaScriptExecutor executorYes = (IJavaScriptExecutor)driver;
            executorYes.ExecuteScript("arguments[0].click();", eleYes);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);




            //Check with value of DB
            //XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            //Assert.AreEqual("0", finalRow.RECORD_STATUS);
            //Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--success")).Displayed);

            driver.Close();
        }

        [TestMethod]
        public void Delete_PTX_CT_With_OK_Status()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ptx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[2]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr["+number+"]/td[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            //String Code = ele.Text;

            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/div/div/button[2]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleDel1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[2]"));
            IJavaScriptExecutor executorDel1 = (IJavaScriptExecutor)driver;
            executorDel1.ExecuteScript("arguments[0].click();", eleDel1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleYes = driver.FindElement(By.XPath("/html/body/div/div/div[4]/div[2]/button"));
            IJavaScriptExecutor executorYes = (IJavaScriptExecutor)driver;
            executorYes.ExecuteScript("arguments[0].click();", eleYes);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);




            //Check with value of DB
            //XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            //Assert.AreEqual("0", finalRow.RECORD_STATUS);
            Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--success")).Displayed);

            driver.Close();

        }


        [TestMethod]
        public void Cancel_Delete_PTX_CT()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/ptx-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[" + number + "]/td[2]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            //String Code = ele.Text;

            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div/div[1]/ul/li[2]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleDel1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[3]"));
            IJavaScriptExecutor executorDel1 = (IJavaScriptExecutor)driver;
            executorDel1.ExecuteScript("arguments[0].click();", eleDel1);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleYes = driver.FindElement(By.XPath("/html/body/div/div/div[4]/div[1]/button"));
            IJavaScriptExecutor executorYes = (IJavaScriptExecutor)driver;
            executorYes.ExecuteScript("arguments[0].click();", eleYes);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);




            //Check with value of DB
            //XE_DTO finalRow = DataProvider.Instance.GetData<XE_DTO>("XE_SEARCH", new { XE_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            //Assert.AreEqual("0", finalRow.RECORD_STATUS);
            //Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--success")).Displayed);

            driver.Close();
        }
    }
}
