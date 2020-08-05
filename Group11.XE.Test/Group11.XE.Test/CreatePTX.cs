using System;
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
    public class CreatePTX : Load
    {

        public CreatePTX()
        {
            driver = new ChromeDriver();
            homeURL = "http://localhost:4200";

        }

        [TestMethod]
        [DataRow("testnew11111","Xe airblade tra qua han")]
        public void Create_PTX_With_OK_Status(string Code,string Note)
        {


            Login();

            
            DateTime Exp_dt = new DateTime(2020, 9, 8);
            DateTime Return_dt = new DateTime(2020, 9, 8);

            decimal Price = 500000M;

            driver.Navigate().GoToUrl(homeURL + "/app/admin/thue-xe-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Name("ptX_CODE")));
            Thread.Sleep(8000);



            //Act;



            driver.FindElement(By.Name("ptX_CODE")).SendKeys(Code);
            driver.FindElement(By.Name("ptX_EXP_DT")).SendKeys((Exp_dt).ToString());
            driver.FindElement(By.Name("ptX_RETURN_DT")).SendKeys(Return_dt.ToString());
            driver.FindElement(By.Name("ptX_PRICE")).SendKeys((Price).ToString());
            driver.FindElement(By.Name("ptX_NOTE")).SendKeys(Note);

            //driver.FindElement(By.Name("xE_STATUS")).SendKeys(Status);

            

            Random rnd = new Random();
            int number = rnd.Next(0, 3);

            IWebElement comboBox1 = driver.FindElement(By.Name("ntX_ID"));
            SelectElement selectedValue1 = new SelectElement(comboBox1);
            selectedValue1.SelectByIndex(number);

            IWebElement comboBox2 = driver.FindElement(By.Name("xE_ID"));
            SelectElement selectedValue2 = new SelectElement(comboBox2);
            selectedValue2.SelectByIndex(number);

            String Ntx_Id = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text;", selectedValue1);
            String Xe_Id = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text;", selectedValue2);

            //String Status = selectedValue.SelectByIndex(0).value;

            //get value of select element



            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[1]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);

            WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait1.Until(e => e.FindElement(By.ClassName("swal-icon--success")));

            //Check with value of DB
            //CM_XE_DTO input = new CM_XE_DTO(Code, Name, Origin);
            //PTX_DTO finalRow = DataProvider.Instance.GetData<PTX_DTO>("PhieuThueXe_Search", new { PTX_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--success")).Displayed);
            




            driver.Close();

        }

        [TestMethod]
        [DataRow("", "Xe airblade tra qua han")]
        public void Create_PTX_With_Empty(String Code, string Note)
        {


            Login();
 

            DateTime Exp_dt = new DateTime(2020, 9, 7);
            DateTime Return_dt = new DateTime(2020, 9, 7);

            decimal Price = 500000M;

            driver.Navigate().GoToUrl(homeURL + "/app/admin/thue-xe-add");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(e => e.FindElement(By.Name("ptX_CODE")));
            Thread.Sleep(8000);



            //Act;



            driver.FindElement(By.Name("ptX_CODE")).Clear();
            driver.FindElement(By.Name("ptX_EXP_DT")).SendKeys((Exp_dt).ToString());
            driver.FindElement(By.Name("ptX_RETURN_DT")).SendKeys(Return_dt.ToString());
            driver.FindElement(By.Name("ptX_PRICE")).SendKeys((Price).ToString());
            driver.FindElement(By.Name("ptX_NOTE")).SendKeys(Note);

            //driver.FindElement(By.Name("xE_STATUS")).SendKeys(Status);



            Random rnd = new Random();
            int number = rnd.Next(0, 3);

            IWebElement comboBox1 = driver.FindElement(By.Name("ntX_ID"));
            SelectElement selectedValue1 = new SelectElement(comboBox1);
            selectedValue1.SelectByIndex(number);

            IWebElement comboBox2 = driver.FindElement(By.Name("xE_ID"));
            SelectElement selectedValue2 = new SelectElement(comboBox2);
            selectedValue2.SelectByIndex(number);

            String Ntx_Id = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text;", selectedValue1);
            String Xe_Id = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text;", selectedValue2);

            //String Status = selectedValue.SelectByIndex(0).value;

            //get value of select element



            IWebElement ele = driver.FindElement(By.XPath("/html/body/app-root/ng-component/div/div/div[2]/ng-component/form/ul/li[1]"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", ele);


            //Check with value of DB
            //CM_XE_DTO input = new CM_XE_DTO(Code, Name, Origin);
            PTX_DTO finalRow = DataProvider.Instance.GetData<PTX_DTO>("PTX_Search", new { PTX_CODE = Code }).ToList().FirstOrDefault();

            //Assert
            Assert.IsTrue(driver.FindElement(By.ClassName("swal-icon--error")).Displayed);
            //Assert.IsNull(finalRow);
            //Assert.AreEqual(Rent_dt, finalRow.PTX_RENT_DT);
            //Assert.AreEqual(Exp_dt, finalRow.PTX_EXP_DT);
            //Assert.AreEqual(Return_dt, finalRow.PTX_RETURN_DT);
            //Assert.AreEqual(Price, finalRow.PTX_PRICE);
            //Assert.AreEqual(Ntx_Id, finalRow.NTX_ID);
            //Assert.AreEqual(Xe_Id, finalRow.XE_ID);


            driver.Close();

        }
    }
}
