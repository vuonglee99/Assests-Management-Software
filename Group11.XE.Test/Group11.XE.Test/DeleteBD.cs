using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Group11.XE.Test.Helper;
using System.Linq;
using Group11.XE.Test.BAODUONGDTO;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Threading;

namespace Group11.XE.Test
{
    [TestClass]
    public class DeleteBD : Load
    {
        public DeleteBD()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        public void Delete_BD_With_OK_Status()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-duong-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr["+number+"]/td[3]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            //String Code = ele.Text;


            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[4]"));
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
        public void Cancel_Delete_BD()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-duong-list");
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


            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[4]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleYes = driver.FindElement(By.XPath("/html/body/div/div/div[4]/div[1]/button"));
            IJavaScriptExecutor executorYes = (IJavaScriptExecutor)driver;
            executorYes.ExecuteScript("arguments[0].click();", eleYes);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);




            //Check with value of DB
            BAODUONG_DTO finalRow = DataProvider.Instance.GetData<BAODUONG_DTO>("BD_SEARCH", new { BD_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            //Assert.AreEqual("0", finalRow.RECORD_STATUS);
            Assert.IsNotNull(finalRow);

            driver.Close();
        }

        [TestMethod]
        public void Delete_BD_CT_With_OK_Status()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-duong-list");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Thread.Sleep(8000);
            //Act
            Random rnd = new Random();
            int number = rnd.Next(1, 5);

            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[3]/div/div/div/div/div/p-table/div/div/div/div[2]/table/tbody/tr[" + number + "]/td[3]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


            //String Code = ele.Text;


            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[2]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleDel1 = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[3]"));
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
        public void Cancel_Delete_BD_CT()
        {
            Login();

            driver.Navigate().GoToUrl(homeURL + "/app/admin/bao-duong-list");
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

            IWebElement eleXem = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/div[1]/ul/li[2]"));
            IJavaScriptExecutor executorXem = (IJavaScriptExecutor)driver;
            executorXem.ExecuteScript("arguments[0].click();", eleXem);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
 
            IWebElement eleDel = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[3]"));
            IJavaScriptExecutor executorDel = (IJavaScriptExecutor)driver;
            executorDel.ExecuteScript("arguments[0].click();", eleDel);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            IWebElement eleNo = driver.FindElement(By.XPath("/html/body/div/div/div[4]/div[1]/button"));
            IJavaScriptExecutor executorNo = (IJavaScriptExecutor)driver;
            executorNo.ExecuteScript("arguments[0].click();", eleNo);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);




            //Check with value of DB
            BAODUONG_DTO finalRow = DataProvider.Instance.GetData<BAODUONG_DTO>("BD_SEARCH", new { BD_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            //Assert.AreEqual("0", finalRow.RECORD_STATUS);
            Assert.IsNotNull(finalRow);

            driver.Close();
        }
    }
}
